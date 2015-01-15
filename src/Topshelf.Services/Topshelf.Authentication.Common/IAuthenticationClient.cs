using System;

namespace Topshelf.Authentication.Common
{
    // 1. add references
    // 2. add config file as content
    // 3. containerBuilder.Register<IAuthenticationClient, AuthenticationClient>
    // 4. container.Resolve<IAuthenticationClient>() += OnStateChanged;
    // 5. container.Resolve<IAuthenticationClient>().Login(settings)
    public interface IAuthenticationClient
    {
        void Login(AuthenticationSettings settings);
        event EventHandler<AuthenticationStateEventArgs> StateChanged;
    }
}