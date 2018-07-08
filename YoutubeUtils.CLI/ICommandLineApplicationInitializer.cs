using Microsoft.Extensions.CommandLineUtils;

namespace YoutubeUtils.CLI
{
    public interface ICommandLineApplicationInitializer
    {
        CommandLineApplication Initializer();
    }
}