using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WareHouse.DemoApp.Core.Domain.Dtos
{
    public class ServerSideError
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public ServerSideError(string errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }

}
