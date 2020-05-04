using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Drawing;

/// <summary>
/// Descripción breve de ExpGrilla
/// </summary>
public class ExpGrilla
{
    public string style;

    public ExpGrilla()
    {
        style = @"<style> 
                    .gridHeader { background-color: #359af2; font-weight: bold; color: White; border: 1px solid #d7e6e9; text-align: center; } 
                    .gridItem   { border: 1px solid #d7e6e9; mso-number-format:\@; }  
                  </style>";
    }

    public StringWriter ObtenerGrilla(GridView GridView1, object LstConsulta)
    {
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            if (LstConsulta != null)
            {
                GridView1.AllowPaging = false;
                GridView1.DataSource = LstConsulta;
                GridView1.DataBind();
            }

            for (int i = 0; i < GridView1.Columns.Count; i++)
            {
                string s = GridView1.Columns[i].HeaderStyle.CssClass;
                if (s == "gridIco")
                    GridView1.Columns[i].Visible = false;
                else
                    GridView1.Columns[i].HeaderStyle.CssClass = "gridHeader";
            }

            GridView1.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in GridView1.HeaderRow.Cells)
            {
                cell.BackColor = GridView1.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in GridView1.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = GridView1.RowStyle.BackColor;
                    }
                    cell.CssClass = "gridItem";
                    List<Control> lstControls = new List<Control>();

                    //Add controls to be removed to Generic List
                    foreach (Control control in cell.Controls)
                    {
                        lstControls.Add(control);
                    }

                    //Loop through the controls to be removed and replace then with Literal
                    foreach (Control control in lstControls)
                    {
                        switch (control.GetType().Name)
                        {
                            case "HyperLink":
                                cell.Controls.Add(new Literal { Text = (control as HyperLink).Text });
                                break;
                            case "TextBox":
                                cell.Controls.Add(new Literal { Text = (control as TextBox).Text });
                                break;
                            case "LinkButton":
                                cell.Controls.Add(new Literal { Text = (control as LinkButton).Text });
                                break;
                            case "CheckBox":
                                cell.Controls.Add(new Literal { Text = (control as CheckBox).Checked ? "1" : "0" });
                                break;
                            case "RadioButton":
                                cell.Controls.Add(new Literal { Text = (control as RadioButton).Text });
                                break;
                            case "DropDownList":
                                cell.Controls.Add(new Literal { Text = (control as DropDownList).SelectedItem.Text });
                                break;
                        }
                        cell.Controls.Remove(control);
                    }
                }
            }

            GridView1.RenderControl(hw);

            return sw;
        }
    }

}