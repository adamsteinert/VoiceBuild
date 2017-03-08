using Vocal.Model;

namespace Vocal.Executors
{
    public interface ILauncher
    {
        IProcessHandle Execute(IVoiceCommand command);
    }
}
