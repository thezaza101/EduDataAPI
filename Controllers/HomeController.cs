using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EduDataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EduDataAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly EDUDataContext _context;

        public HomeController(EDUDataContext context)
        {
            _context = context;    
        }

        public IActionResult Index(HomeViewModel _homeViewModel)
        {
            ViewData["Message"] = "Element: " + _homeViewModel.DataID;
            if (string.IsNullOrWhiteSpace(_homeViewModel.changeAction))
            {
                IniViewData();
                _homeViewModel = populateViewModelWithDataElement(_homeViewModel);
            } else if (_homeViewModel.changeAction.ToUpper()=="U")
            {
                DataElement dataElement = _context.DataElement.Include("datatype").Include("datatype.facets").SingleOrDefault(u => u.identifier.Substring(u.identifier.LastIndexOf(@"/")+1).Equals(_homeViewModel.DataID));
                dataElement.name = _homeViewModel.name;
                dataElement.domain = _homeViewModel.domain;
                dataElement.status = _homeViewModel.status;
                dataElement.definition = _homeViewModel.definition;
                dataElement.guidance = _homeViewModel.guidance;
                dataElement.identifier = _homeViewModel.identifier;
                dataElement.sourceURL = _homeViewModel.sourceURL;
                dataElement.version = _homeViewModel.version;

                Change currentChange = new Change() {ChangeType = _homeViewModel.changeAction, UpdatedElement = dataElement};

                ChangeSet cs = _context.ChangeSet.Include("Changes").SingleOrDefault(c => c.ID.Equals(_homeViewModel.changeSetID.Trim()));

                if (cs.Changes == null)
                {
                    cs.Changes = new List<Change>();
                }

                cs.Changes.Add(currentChange);
                _context.Update(cs);
                _context.SaveChanges();

            }

            return View();
        }
        
        private void IniViewData()
        {
            ViewData["name"] = "";
            ViewData["domain"] = "";
            ViewData["status"] = "";
            ViewData["definition"] = "";
            ViewData["guidance"] = "";
            ViewData["identifier"] = "";
            ViewData["usage"] = "";
            ViewData["dataTypeFacetPattern"] = "";
            ViewData["dataTypeFacetMaxLength"] = "";
            ViewData["dataTypeFacetMinInclusive"] = "";
            ViewData["dataTypeFacetMinLength"] = "";
            ViewData["dataTypeType"] = "";
            ViewData["dataTypeValues"] = "";
            ViewData["values"] = "";
            ViewData["sourceURL"] = "";
            ViewData["version"] = "";

            ViewData["changeSetID"] = "";
            ViewData["changeAction"] = "";
            ViewData["changeUpdateDescription"] = "";
            ViewData["changeUpdatedDomain"] = "";
            ViewData["changeNumber"] = "";
            ViewData["changeSetAction"] = "";
        }
        private HomeViewModel populateViewModelWithDataElement (HomeViewModel _homeViewModel)
        {
            DataElement dataElement;

            if (string.IsNullOrWhiteSpace(_homeViewModel.DataID))
            {
                ViewData["ActionResult"] = "No data element in focus";
            }else{
                dataElement = _context.DataElement.Include("datatype").Include("datatype.facets").SingleOrDefault(u => u.identifier.Substring(u.identifier.LastIndexOf(@"/")+1).Equals(_homeViewModel.DataID));
                if (dataElement==null)
                {
                    ViewData["ActionResult"] = "Data Element not found";
                } else {
                    ViewData["ActionResult"] = "Data Element "+dataElement.name + " in focus";
                    _homeViewModel.ElemtnInFocus = dataElement;
                    _homeViewModel.name = dataElement.name;
                    _homeViewModel.domain = dataElement.domain;
                    _homeViewModel.status = dataElement.status;
                    _homeViewModel.definition = dataElement.definition;
                    _homeViewModel.guidance = dataElement.guidance;
                    _homeViewModel.identifier = dataElement.identifier;
                    _homeViewModel.usage = dataElement.usage;
                    _homeViewModel.dataTypeFacetPattern = dataElement.datatype.facets.pattern;
                    _homeViewModel.dataTypeFacetMaxLength = dataElement.datatype.facets.maxLength;
                    _homeViewModel.dataTypeFacetMinInclusive = dataElement.datatype.facets.minInclusive;
                    _homeViewModel.dataTypeFacetMinLength = dataElement.datatype.facets.minLength;
                    _homeViewModel.dataTypeType = dataElement.datatype.type;
                    _homeViewModel.dataTypeValues = dataElement.datatype.values;
                    _homeViewModel.values = dataElement.values;
                    _homeViewModel.sourceURL = dataElement.sourceURL;
                    _homeViewModel.version = dataElement.version;



                    ViewData["name"] = _homeViewModel.name;
                    ViewData["domain"] = _homeViewModel.domain;
                    ViewData["status"] = _homeViewModel.status;
                    ViewData["definition"] = _homeViewModel.definition;
                    ViewData["guidance"] = _homeViewModel.guidance;
                    ViewData["identifier"] = _homeViewModel.identifier;
                    ViewData["usage"] = _homeViewModel.usage;
                    ViewData["dataTypeFacetPattern"] = _homeViewModel.dataTypeFacetPattern;
                    ViewData["dataTypeFacetMaxLength"] = _homeViewModel.dataTypeFacetMaxLength;
                    ViewData["dataTypeFacetMinInclusive"] = _homeViewModel.dataTypeFacetMinInclusive;
                    ViewData["dataTypeFacetMinLength"] = _homeViewModel.dataTypeFacetMinLength;
                    ViewData["dataTypeType"] = _homeViewModel.dataTypeType;
                    ViewData["dataTypeValues"] = _homeViewModel.dataTypeValues;
                    ViewData["values"] = _homeViewModel.values;
                    ViewData["sourceURL"] = _homeViewModel.sourceURL;
                    ViewData["version"] = _homeViewModel.version;
                }
            }     

            return _homeViewModel;
        }
















        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact(HomeViewModel _homeViewModel)
        {
            ViewData["Message"] = "Your contact page.";
            ViewData["Message"] = "Element: " + _homeViewModel.DataID;
            if (string.IsNullOrWhiteSpace(_homeViewModel.changeSetAction))
            {
                IniViewData();
            } else if (_homeViewModel.changeSetAction.ToUpper()=="A") {
                CSResolve(_homeViewModel);
            } else if (_homeViewModel.changeSetAction.ToUpper()=="D") {
                CSResolve(_homeViewModel);
            } else if (_homeViewModel.changeSetAction.ToUpper()=="U") {
                CSResolve(_homeViewModel);
            } else if (_homeViewModel.changeSetAction.ToUpper()=="S") {
                CSResolve(_homeViewModel);
            }
            return View();
        }

        public IActionResult CSFind(HomeViewModel _homeViewModel)
        {
            ViewData["CSMessage"] = "Element: " + _homeViewModel.DataID;
            IniViewData();
            _homeViewModel = populateViewModelWithDataElement(_homeViewModel);
            return View();
        }
        public IActionResult CSResolve(HomeViewModel _homeViewModel)
        {
            if (_homeViewModel.changeSetAction.Equals("A"))
            {
                if (string.IsNullOrWhiteSpace(_homeViewModel.changeUpdatedDomain) | string.IsNullOrWhiteSpace(_homeViewModel.changeSetID) | string.IsNullOrWhiteSpace(_homeViewModel.changeUpdateDescription))
                {
                    ViewData["CSMessage"] = "Changeset: fill out mandatry fields";
                    return View();
                } 
                ChangeSet cs = new ChangeSet();
                cs.ID = _homeViewModel.changeSetID;
                cs.UpdatedDomain = _homeViewModel.changeUpdatedDomain;
                cs.UpdateDescription = _homeViewModel.changeUpdateDescription;

                _context.Add(cs);
                _context.SaveChanges();
                ViewData["CSMessage"] = "Changeset: " + _homeViewModel.changeSetID + " added";

                return View();


            }
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
