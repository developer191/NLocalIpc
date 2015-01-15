namespace Topshelf.Services.Events.Common.Configuration
{
    public interface ITransportConfigProvider
    {
        TransportSettings Read();
    }
}