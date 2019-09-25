using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoPostProject.WebUI.Models
{
    public enum FolderPath
    {
        UserProfil,
        UserCoverImage,
        Video
    }

    public class FxFunction
    {
        public static string ImageUpload(HttpPostedFileBase resim, FolderPath folderPath, out bool isComplated )
        {
            string errorText = null;
            if (resim != null)
            {
                if (resim.ContentLength <= 2097152)
                {
                    if (resim.ContentType.Contains("image"))
                    {
                        string uploadPath = $"{folderPath.ToString()}/{Guid.NewGuid().ToString().Replace('-', '_').ToLower()}.{resim.ContentType.Split('/')[1]}";
                        resim.SaveAs(HttpContext.Current.Server.MapPath("~/Content/uploads/" + uploadPath));
                        isComplated = true;
                        return uploadPath;
                    }
                    else
                    {
                        errorText = $"Lütfen sadece resim formatında bir dosya seçin.";
                    }
                }
                else
                {
                    errorText = $"Seçtiğiniz resim 2mb boyutundan büyük olduğundan yüklenemez. Lütfen 2mb boyutundan küçük resim seçin.";
                }
            }
            else
            {
                errorText = $"Resim yüklemek için bir resim seçmelisiniz.";
            }

            isComplated = false;
            return errorText;
        }

        public static string VideoUpload(HttpPostedFileBase video, FolderPath folderPath, out bool isComplated)
        {
            string errorText = null;
            if (video != null)
            {
                if (video.ContentLength > 0)
                {
                    if (video.ContentType.Contains("video"))
                    {
                        string uploadPath = $"{folderPath.ToString()}/{Guid.NewGuid().ToString().Replace('-', '_').ToLower()}.{video.ContentType.Split('/')[1]}";
                        video.SaveAs(HttpContext.Current.Server.MapPath("~/Content/uploads/" + uploadPath));
                        isComplated = true;
                        return uploadPath;
                    }
                    else
                    {
                        errorText = $"Lütfen sadece video formatında bir dosya seçin.";
                    }
                }
                else
                {
                    errorText = $"";
                }
            }
            else
            {
                errorText = $"Resim yüklemek için bir resim seçmelisiniz.";
            }

            isComplated = false;
            return errorText;
        }
    }
}