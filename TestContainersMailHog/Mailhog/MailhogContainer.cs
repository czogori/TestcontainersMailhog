using System.Net.Http.Json;
using System.Net.Mail;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.Extensions.Logging;

namespace TestContainersMailHog.Mailhog;

public class MailhogContainer : DockerContainer
{
    public MailhogContainer(IContainerConfiguration configuration) : base(configuration)
    {
    }

    public MailhogContainer(IContainerConfiguration configuration, ILogger logger) : base(configuration, logger)
    {
    }
    
    public ushort GetMappedServicePublicPort()
    {
        return GetMappedPublicPort(1025);
    }
    
    public ushort GetMappedUiPublicPort()
    {
        return GetMappedPublicPort(8025);
    }

    public SmtpClient GetSmtpClient()
    {
        return new SmtpClient(Hostname)
        {
            Port = GetMappedServicePublicPort(),
        };
    }
    
    public async Task<MailhogMessagesResponse> CallEndpointMessages()
    {
        var endpoint = "/api/v2/messages";
        var httpClient = new HttpClient();
        var requestUri = new UriBuilder(Uri.UriSchemeHttp, Hostname, GetMappedUiPublicPort(), endpoint).Uri;
        var response = await httpClient.GetAsync(requestUri)
            .ConfigureAwait(false);
        return await response.Content.ReadFromJsonAsync<MailhogMessagesResponse>(); 
    }
}