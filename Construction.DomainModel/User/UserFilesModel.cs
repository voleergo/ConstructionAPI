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
    public class UserFilesModel : BaseModel
    {
        public long ID_Events { get; set; }
        public long ID_UserFiles { get; set; }


        public string FileURL { get; set; }

        public string EventImageUrl { get; set; }
        public string FileType { get; set; }
        public string TmpID { get; set; }
        public string FileName { get; set; }
        public string FileExtention { get; set; }

        public UserFilesModel()
        {
            ID_UserFiles = 0;
            ID_Events = 0;
            FileURL = string.Empty;
            EventImageUrl = string.Empty;
            FileType = string.Empty;
            FileName = string.Empty;
            FileExtention = string.Empty;
            TmpID = string.Empty;
        }
    }
}