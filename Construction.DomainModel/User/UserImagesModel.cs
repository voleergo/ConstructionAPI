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
    public class UserImagesModel : BaseModel
    {
        public long ID_Events { get; set; }
        public long ID_UserImages { get; set; }


        public string ImageURL { get; set; }

        public string EventImageUrl { get; set; }
        public string ImageType { get; set; }
        public int TmpID { get; set; }
        public string ImageName { get; set; }
        public string ImageExtention { get; set; }

        public UserImagesModel()
        {
            ID_UserImages = 0;
            ID_Events = 0;
            ImageURL = string.Empty;
            EventImageUrl = string.Empty;
            ImageType = string.Empty;
            ImageName = string.Empty;
            ImageExtention = string.Empty;
            TmpID = 0;
        }

        public static void Add(UserImagesModel userData)
        {
            throw new NotImplementedException();
        }
    }
}