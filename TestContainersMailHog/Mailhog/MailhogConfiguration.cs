using Docker.DotNet.Models;
using DotNet.Testcontainers.Configurations;

namespace TestContainersMailHog.Mailhog;

public class MailhogConfiguration : ContainerConfiguration
{
    public MailhogConfiguration()
    {
        
    }
    public MailhogConfiguration(IResourceConfiguration<CreateContainerParameters> configuration) : base(configuration)
    {
    }
    
    public MailhogConfiguration(IContainerConfiguration configuration) : base(configuration)
    {
    }

    public MailhogConfiguration(MailhogConfiguration oldValue, MailhogConfiguration newValue) : base(oldValue, newValue)
    {
    }

}