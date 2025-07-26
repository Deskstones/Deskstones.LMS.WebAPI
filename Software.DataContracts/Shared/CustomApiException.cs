using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Software.DataContracts.Shared
{
    public class CustomApiException : Exception
    {
        public CustomApiException(string message): base(message)
        {
        }
    }
}
