using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc4WithStoreProcedure.DataAccess;
using Mvc4WithStoreProcedure.Models;

namespace Mvc4WithStoreProcedure.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/

        AlmacenEntities db = new AlmacenEntities();           

        [HttpGet]
        public ActionResult InsertCustomer()
        {
            ViewBag.TableList = db.sexo.ToList();

            return View();
        }
        [HttpPost]
        public ActionResult InsertCustomer(ViewModelCustomer objCustomer, string ID)
        {

            objCustomer.Birthdate = Convert.ToDateTime(objCustomer.Birthdate);
            if (ModelState.IsValid) //checking model is valid or not  
            {
                objCustomer.sexo = Convert.ToInt32(ID);
                DataAccessLayer objDB = new DataAccessLayer();
                string result = objDB.InsertData(objCustomer);
                //ViewData["result"] = result;  
                TempData["result1"] = result;
                ModelState.Clear(); //clearing model  
                //return View();  
                return RedirectToAction("ShowAllCustomerDetails");
            }

            else
            {
                ModelState.AddModelError("", "Error in saving data");
                return View();
            }
        }

        [HttpGet]
        public ActionResult ShowAllCustomerDetails()
        {
            ViewModelCustomer objCustomer = new ViewModelCustomer();
            DataAccessLayer objDB = new DataAccessLayer(); //calling class DBdata  
            objCustomer.ShowallCustomer = objDB.Selectalldata();
            return View(objCustomer);
        }
        [HttpGet]
        public ActionResult Details(string ID)
        {
            //Customer objCustomer = new Customer();  
            //DataAccessLayer objDB = new DataAccessLayer(); //calling class DBdata  
            //objCustomer.ShowallCustomer = objDB.Selectalldata();  
            //return View(objCustomer);  
            ViewModelCustomer objCustomer = new ViewModelCustomer();
            DataAccessLayer objDB = new DataAccessLayer(); //calling class DBdata  
            return View(objDB.SelectDatabyID(ID));
        }
        [HttpGet]
        public ActionResult Edit(string ID)
        {
            ViewModelCustomer objCustomer = new ViewModelCustomer();
            DataAccessLayer objDB = new DataAccessLayer(); //calling class DBdata  
            return View(objDB.SelectDatabyID(ID));
        }

        [HttpPost]
        public ActionResult Edit(ViewModelCustomer objCustomer)
        {
            objCustomer.Birthdate = Convert.ToDateTime(objCustomer.Birthdate);
            if (ModelState.IsValid) //checking model is valid or not  
            {
                DataAccessLayer objDB = new DataAccessLayer(); //calling class DBdata  
                string result = objDB.UpdateData(objCustomer);
                //ViewData["result"] = result;  
                TempData["result2"] = result;
                ModelState.Clear(); //clearing model  
                //return View();  
                return RedirectToAction("ShowAllCustomerDetails");
            }
            else
            {
                ModelState.AddModelError("", "Error in saving data");
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(String ID)
        {
            DataAccessLayer objDB = new DataAccessLayer();
            int result = objDB.DeleteData(ID);
            TempData["result3"] = result;
            ModelState.Clear(); //clearing model  
            //return View();  
            return RedirectToAction("ShowAllCustomerDetails");
        }
    }

}
