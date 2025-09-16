/*----------------------------------- UserImagesModel Class-----------------------------------------------------------------------------------------------------------------------
Purpose    : UserImagesModel Class
Author     : Jinesh Kumar C
Copyright  :
Created on :24-11-2023 09:36:44
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
MODIFICATIONS 
On                                     By
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
24-11-2023 09:36:44                Jinesh Kumar C
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Construction.DomainModel.Common;

namespace Construction.DomainModel.User
{
    public class UserUploadModel : BaseModel
    {
        public long ID_UserImages { get; set; }
        public string ImageURL { get; set; }
        public string ImageType { get; set; }

        public string ImageName { get; set; }

        public UserUploadModel()
        {
            ID_UserImages = 0;
            ImageURL = string.Empty;
            ImageType = string.Empty;
            ImageName = string.Empty;
        }
    }
}
