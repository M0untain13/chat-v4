using Microsoft.Extensions.Logging;
using MvvmCross.Platforms.Wpf.Core;

namespace ClientApp;

public class Setup : MvxWpfSetup<ClientCore.CoreApp>
{
    protected override ILoggerProvider? CreateLogProvider() => null;
    protected override ILoggerFactory? CreateLogFactory() => null;
}