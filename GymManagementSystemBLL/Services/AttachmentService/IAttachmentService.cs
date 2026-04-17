using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementSystemBLL.Services.AttachmentService
{
    public interface IAttachmentService
    {
        string? Upload (string foldername , IFormFile file);
        bool Delete (string filename , string foldername );
    }
}
