using System;

namespace MultiBuffer.WpfApp.Models.Interfaces
{
    public interface ITextMessageNotifyController
    {

        event Action<string, string> CopyIsActive;

        event Action<string, string> PasteIsActive;
    }
}
