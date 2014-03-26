using BLL;
using GatorBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Movie.Controllers
{
    public class HunterInfoController : Controller
    {
        static List<HunterInfo> hunterInfos = new List<HunterInfo>();
        //static List<HunterInfo> finalHunterInfos = new List<HunterInfo>();
        //
        // GET: /HunterInfo/
        public ActionResult Index()
        {
            List<HunterInfo> hunterInfoIndexList = GetAllHunterInfos();
            return View(hunterInfoIndexList);
        }
        public List<HunterInfo> GetAllHunterInfos()
        {
            List<HunterInfo> hunterInfoList = new List<HunterInfo>();
            List<HunterInfoVM> newHunterInfo = new List<HunterInfoVM>();
            Logic logic = new Logic();
            HunterInfo hunterInfo = new HunterInfo();
            newHunterInfo = logic.GetAllHunterInfos();
            foreach (HunterInfoVM hunterInfoVm in newHunterInfo)
            {
                hunterInfo = ConvertHunterInfoVmToHunterInfo(hunterInfoVm);
                bool x = hunterInfoList.Contains(hunterInfo);
                if (x == false)
                {
                    hunterInfoList.Add(hunterInfo);
                }
            }
            return hunterInfoList;
        }
        public HunterInfo ConvertHunterInfoVmToHunterInfo(HunterInfoVM hunterInfoVm)
        {
            HunterInfo hunterInfo = new HunterInfo();
            if (hunterInfoVm != null)
            {
                hunterInfo.id = hunterInfoVm.id;
                hunterInfo.lastName = hunterInfoVm.lastName;
                hunterInfo.firstName = hunterInfoVm.firstName;
                hunterInfo.email = hunterInfoVm.email;
                hunterInfo.street = hunterInfoVm.street;
                hunterInfo.city = hunterInfoVm.city;
                hunterInfo.state = hunterInfoVm.state;
                hunterInfo.zip = hunterInfoVm.zip;
                hunterInfo.addressType = hunterInfoVm.addressType;
                hunterInfo.number = hunterInfoVm.number;
                hunterInfo.phoneType = hunterInfoVm.phoneType;
            }
            return hunterInfo;
        }
        //
        // GET: /HunterInfo/Details/5
        public ActionResult Details(HunterInfo hunterInfo)
        {
            return View(hunterInfo);
        }

        //
        // GET: /HunterInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /HunterInfo/Create
        [HttpPost]
        public ActionResult Create(HunterInfo hunterInfo)
        {
            Logic logic = new Logic();
            if (!ModelState.IsValid)
            {
                return View("Create", hunterInfo);
            }
            logic.CreateHunterInfoVM(hunterInfo.lastName, hunterInfo.firstName, hunterInfo.email,
                hunterInfo.street, hunterInfo.city, hunterInfo.state, hunterInfo.zip, hunterInfo.addressType,
                hunterInfo.number, hunterInfo.phoneType);
            hunterInfos.Add(hunterInfo);
            return RedirectToAction("Index");
        }

        //
        // GET: /HunterInfo/Edit/5
        public ActionResult Edit(HunterInfo hunterInfo)
        {
            return View(hunterInfo);
        }

        //
        // POST: /HunterInfo/Edit/5
        [HttpPost]
        public ActionResult Edit(HunterInfo hunterInfo, FormCollection collection)
        {
            List<HunterInfo> hunterInfoList = GetAllHunterInfos();
            Logic logic = new Logic();
            if (!ModelState.IsValid)
            {
                return View("Edit", hunterInfo);
            }
            foreach (HunterInfo us in hunterInfoList)
            {
                if (us.id == hunterInfo.id)
                {
                    us.id = hunterInfo.id;
                    us.lastName = hunterInfo.lastName ?? "";
                    us.firstName = hunterInfo.firstName ?? "";
                    us.email = hunterInfo.email ?? "";
                    us.street = hunterInfo.street ?? "";
                    us.city = hunterInfo.city ?? "";
                    us.state = hunterInfo.state ?? "";
                    us.zip = hunterInfo.zip ?? "";
                    us.addressType = hunterInfo.addressType ?? "";
                    us.number = hunterInfo.number ?? "";
                    us.phoneType = hunterInfo.phoneType ?? "";
                    logic.UpdateHunterInfo(us.id, us.lastName, us.firstName, us.email,
                        us.street, us.city, us.state, us.zip, us.addressType,
                        us.number, us.phoneType);
                }
            }
            return RedirectToAction("Index");
        }

        //
        // GET: /HunterInfo/Delete/5
        public ActionResult Delete(HunterInfo hunterInfo)
        {
            return View(hunterInfo);
        }

        //
        // POST: /HunterInfo/Delete/5
        [HttpPost]
        public ActionResult Delete(HunterInfo hunterInfo, FormCollection collection)
        {
            List<HunterInfoVM> newHunterInfo = new List<HunterInfoVM>();
            Logic logic = new Logic();
            newHunterInfo = logic.GetAllHunterInfos();
            foreach (HunterInfoVM hu in newHunterInfo)
            {
                if (hu.id == hunterInfo.id)
                {
                    logic.DeleteHunterInfo(hu.id);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
