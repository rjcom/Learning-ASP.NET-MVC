using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Jq.Grid;

namespace JQCustomerGrid.Models
{
    public class CustomersGridModel
    {
        public CustomersGridModel()
        {
            CustomersGrid = new JQGrid
                {
                    Columns = new List<JQGridColumn>()
                                 {
                                     new JQGridColumn { DataField = "CustomerID", 
                                                        // always set PrimaryKey for Add,Edit,Delete operations
                                                        // if not set, the first column will be assumed as primary key
                                                        PrimaryKey = true,
                                                        Editable = false,
                                                        HeaderText = "ID",
                                                        TextAlign = Jq.Grid.TextAlign.Center,
                                                        Width = 50
                                                        },
                                     new JQGridColumn { DataField = "CompanyName", 
                                                        Editable = true,
                                                        HeaderText = "Company",
                                                        Width = 175
                                                        },
                                     new JQGridColumn { DataField = "ContactName", 
                                                        Editable = true,
                                                        HeaderText = "Contact",
                                                        Width = 125 },
                                     new JQGridColumn { DataField = "Address", 
                                                        Editable = true,
                                                        HeaderText = "Address",
                                                        Width = 175
                                                        },
                                     new JQGridColumn { DataField = "City", 
                                                        Editable = true,
                                                        HeaderText = "City",
                                                        Width = 100
                                                        },
                                     new JQGridColumn { DataField = "Region", 
                                                        Editable = true,
                                                        HeaderText = "State",
                                                        Width = 70
                                                        },
                                     new JQGridColumn { DataField = "PostalCode", 
                                                        Editable = true,
                                                        HeaderText = "Zip",
                                                        Width = 70
                                                        },
                                     new JQGridColumn { DataField = "Phone", 
                                                        Editable = true,
                                                        HeaderText = "Phone",
                                                        Width = 100
                                                        }
                                 },
                    Height = Unit.Percentage(100),
                    Width = Unit.Percentage(100)
                };

            CustomersGrid.ToolBarSettings.ShowRefreshButton = true;
        }

        public JQGrid CustomersGrid { get; set; }
    }
}