using Docker.DotNet.Models;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;

namespace TestContainersMailHog.Mailhog;

public class MailhogBuilder : ContainerBuilder<MailhogBuilder, MailhogContainer, MailhogConfiguration>
{
    private const int ServicePort = 1025;
    private const int UiPort = 8025;
    
    public MailhogBuilder() : this(new MailhogConfiguration())
    {
        DockerResourceConfiguration = Init().DockerResourceConfiguration;
    }
    public MailhogBuilder(MailhogConfiguration dockerResourceConfiguration) : base(dockerResourceConfiguration)
    {
        DockerResourceConfiguration = dockerResourceConfiguration;
    }
    
    

    public override MailhogContainer Build()
    {
        return new MailhogContainer(DockerResourceConfiguration);
    }

    protected override MailhogBuilder Init()
    {
        return base.Init()
            .WithImage("mailhog/mailhog")
            .WithPortBinding(ServicePort, true)
            .WithPortBinding(UiPort, true)
            .WithWaitStrategy(Wait.ForUnixContainer()
                .UntilHttpRequestIsSucceeded(r => r.ForPort(UiPort)));
    }

    protected override MailhogBuilder Clone(IResourceConfiguration<CreateContainerParameters> resourceConfiguration)
    {
        return Merge(DockerResourceConfiguration, new MailhogConfiguration(resourceConfiguration));
    }
    
    protected override MailhogBuilder Clone(IContainerConfiguration resourceConfiguration)
    {
        return Merge(DockerResourceConfiguration, new MailhogConfiguration(resourceConfiguration));
    }

    protected override MailhogBuilder Merge(MailhogConfiguration oldValue, MailhogConfiguration newValue)
    {
        return new MailhogBuilder(new MailhogConfiguration(oldValue, newValue));
    }

    protected override MailhogConfiguration DockerResourceConfiguration { get; }
}