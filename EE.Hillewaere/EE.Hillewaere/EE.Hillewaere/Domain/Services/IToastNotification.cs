using System;
using System.Collections.Generic;
using System.Text;

namespace EE.Hillewaere.Domain.Services
{
    public interface IToastNotification
    {
        void Show(string message);
    }
}
