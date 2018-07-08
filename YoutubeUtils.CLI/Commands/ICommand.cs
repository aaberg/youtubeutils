using Microsoft.Extensions.CommandLineUtils;

namespace YoutubeUtils.CLI.Commands
{
    public interface ICommand
    {
        void Configure(CommandLineApplication cmd);
    }
}