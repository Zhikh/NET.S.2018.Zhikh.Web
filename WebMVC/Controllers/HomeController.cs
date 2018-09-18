using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVC.Helper;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private const int PageSize = 3;
        private DBModel DBModel;

        public HomeController()
        {
            DBModel = new DBModel();
        }

        public ActionResult Index()
        {
            var model = new List<PictureModel>();

            foreach (var item in DBModel.Pictures)
            {
                model.Add(new PictureModel
                {
                    Id = item.Id,
                    Path = item.Path,
                    PictureDescription = item.PictureDescription,
                    Content = item.BinaryPic,
                    IsSnail = item.IsSnail,
                    Name = item.Name
                });
            }

            return View(model);
        }

        public ActionResult GetImage(int id)
        {
            var imageData = DBModel.Pictures.FirstOrDefault(p => p.Id == id);

            byte[] content = null;

            using (FileStream st = System.IO.File.OpenRead(imageData.Path))
            {
                content = new byte[st.Length];

                st.Read(content, 0, content.Length);
            }

            return File(content, "image/jpg");
        }

        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string ImageName = System.IO.Path.GetFileName(file.FileName);
                string physicalPath = Server.MapPath("~/Content/Images/" + ImageName);
                
                file.SaveAs(physicalPath);

                DBModel.Pictures.Add(
                    new Picture
                    {
                        Name = ImageName,
                        Path = physicalPath,
                        PictureDescription = "From client",
                        IsSnail = false
                    });
                DBModel.SaveChanges();
            }
            
            return RedirectToAction("../home/Display/");
        }

        public ActionResult Display(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            var model = new List<PictureModel>();

            foreach (var item in DBModel.Pictures)
            {
                model.Add(new PictureModel
                {
                    Id = item.Id,
                    Path = item.Path,
                    PictureDescription = item.PictureDescription,
                    Content = item.BinaryPic,
                    IsSnail = item.IsSnail,
                    Name = item.Name
                });
            }

            return View(model.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult DisplayImageInfo()
        {
            return View();
        }

        public ActionResult GetImagesInfo(int page = 1, int pageSize = 1)
        {
            var model = new List<PictureModel>();

            foreach (var item in DBModel.Pictures)
            {
                model.Add(new PictureModel
                {
                    Id = item.Id,
                    Path = item.Path,
                    PictureDescription = item.PictureDescription,
                    Content = item.BinaryPic,
                    IsSnail = item.IsSnail,
                    Name = item.Name
                });

            }

            var pagedData = Pagination.PagedResult(model, page, pageSize);

            return Json(pagedData, JsonRequestBehavior.AllowGet);
        }
    }
}