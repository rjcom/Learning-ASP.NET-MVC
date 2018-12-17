using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JQCustomerGrid.Models;
using Jq.Grid;

namespace JQCustomerGrid.Controllers
{
    // get Jq.Grid from Nuget: Install-Package Jq.Grid
    // ref: http://aiskahendra.wordpress.com/2013/02/06/asp-net-mvc-jqgrid-nuget/
    // also Install-Package Microsoft.AspNet.Web.Optimization

    public class CustomersController : Controller
    {
        private NorthwindEntities db = new NorthwindEntities();

        public ActionResult Index()
        {
            var gridModel = new CustomersGridModel();
            var grid = gridModel.CustomersGrid;
            SetUpGrid(grid);

            return View(gridModel);
        }

        private void SetUpGrid(JQGrid grid)
        {
            grid.ID = "CustomersGrid";
            grid.DataUrl = Url.Action("DataRequested");
            grid.SortSettings.AutoSortByPrimaryKey = false;
            grid.SortSettings.InitialSortColumn = "CompanyName";
            grid.SortSettings.InitialSortDirection = SortDirection.Asc;

            grid.ToolBarSettings.ShowEditButton = true;
            grid.ToolBarSettings.ShowAddButton = true;
            grid.ToolBarSettings.ShowDeleteButton = true;
            grid.EditDialogSettings.CloseAfterEditing = true;
            grid.AddDialogSettings.CloseAfterAdding = true;

            SetUpVirtualScrollingGrid(grid);

            // for inline edit:
            grid.EditUrl = Url.Action("EditRows");
            grid.ClientSideEvents.RowSelect = "editRow";

            // setup the dropdowns
            SetUpStatesColumn(grid);

        }

        private void SetUpStatesColumn(JQGrid grid)
        {
            // setup the grid search criteria for the columns
            JQGridColumn column = grid.Columns.Find(c => c.DataField == "Region");
            column.Editable = true;
            column.EditType = EditType.DropDown;

            // Populate the search dropdown only on initial request, in order to optimize performance
            if (grid.AjaxCallBackMode == AjaxCallBackMode.RequestData)
            {
                string[] states = new string[] { "Alabama", "California", "New Hampshire" };
                List<SelectListItem> list = new List<SelectListItem>(states.Length);
                foreach (string state in states)
                {
                    list.Add(new SelectListItem { Text = state, Value = state });
                }
                column.EditList = list;
            }
        }

        private void SetUpVirtualScrollingGrid(JQGrid grid)
        {
            grid.PagerSettings.ScrollBarPaging = true;
            grid.PagerSettings.PageSize = 20;
            grid.Height = System.Web.UI.WebControls.Unit.Pixel(400);
        }

        // This method is called when the grid requests data
        public JsonResult DataRequested()
        {
            CustomersGridModel gridModel = new CustomersGridModel();
            var data = from c in db.Customers
                       select new
                       {
                           c.CustomerID,
                           c.CompanyName,
                           c.ContactName,
                           c.Address,
                           c.City,
                           c.Region,
                           c.PostalCode,
                           c.Phone
                       };
            return gridModel.CustomersGrid.DataBind(data);
        }

        public void EditRows(Customer editedCustomer)
        {
            CustomersGridModel gridModel = new CustomersGridModel();

            // If we are in "Edit" mode
            if (gridModel.CustomersGrid.AjaxCallBackMode == AjaxCallBackMode.EditRow)
            {
                Customer customer = db.Customers.Single(o => o.CustomerID == editedCustomer.CustomerID);

                customer.Address = editedCustomer.Address;
                customer.City = editedCustomer.City;
                customer.CompanyName = editedCustomer.CompanyName;
                customer.ContactName = editedCustomer.ContactName;
                customer.Phone = editedCustomer.Phone;
                customer.PostalCode = editedCustomer.PostalCode;
                customer.Region = editedCustomer.Region;

                db.SaveChanges();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
