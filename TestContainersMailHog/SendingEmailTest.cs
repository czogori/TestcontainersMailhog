using FluentAssertions;
using TestContainersMailHog.Mailhog;

namespace TestContainersMailHog;

public class SendingEmailTest : IAsyncLifetime
{
    private readonly MailhogContainer container = new MailhogBuilder().Build();
    private int mappedServicePort => container.GetMappedServicePublicPort();
    private int mappedUiPort => container.GetMappedUiPublicPort();
    
    [Fact]
    public async Task Test1()
    {
        // act
        container
            .GetSmtpClient()
            .Send("test@test.pl", "foo@bar.pl", "subject", "body");
        var mailhogMessages = await container.CallEndpointMessages(); 

        // assert
        mailhogMessages.Total.Should().Be(1);
    }

    public async Task InitializeAsync()
    {
        await container.StartAsync().ConfigureAwait(false);
    }

    public async Task DisposeAsync()
    {
        await container.DisposeAsync();
    }
}