﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Administrators_CybrarianQueue : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.BindNoMetadataGridView();
            this.BindUnrecognizedModelsGridView();


        }
    }


    private void BindNoMetadataGridView()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PID");
        dt.Columns.Add("Title");
        dt.Columns.Add("SubmitterEmail");
        dt.Columns.Add("UploadedDate");

       // this.ModelsWithoutMetadataGridView.DataSource = dt;
        this.ModelsWithoutMetadataGridView.DataSource = this.GetModelsWithoutMetadata();
        this.ModelsWithoutMetadataGridView.DataBind();

    }

  

    private void BindUnrecognizedModelsGridView()
    {
        this.UnrecognizedModelsGridView.DataSource = this.GetUnrecognizedModels();
        this.UnrecognizedModelsGridView.DataBind();


    }


  

    private DataTable GetUnrecognizedModels()
    {
        return this.CreateModelsDataTableTestData();
    }


    private DataTable GetModelsWithoutMetadata()
    {
        return this.CreateModelsDataTableTestData();
    }


    //TODO: Remove, test data for screenshots
    private DataTable CreateModelsDataTableTestData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("PID");
        dt.Columns.Add("Title");
        dt.Columns.Add("SubmitterEmail");
        dt.Columns.Add("UploadedDate");

        DataRow row = dt.Rows.Add();
        row["PID"] = "adl:10";
        row["Title"] = "Fighter Jet";
        row["SubmitterEmail"] = "user@problemsolutions.net";
        row["UploadedDate"] = "6/15/2010";
        
        return dt;

    }

    
    protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string pid = e.CommandArgument.ToString();

        switch (e.CommandName)
        {
            case "EditModel":

                //redirect to the model details pages
                Response.Redirect(Website.Common.FormatEditUrl(pid));


                break;

            case "Download":

                var factory = new vwarDAL.DataAccessFactory();
                vwarDAL.IDataRepository vd = factory.CreateDataRepositorProxy();
                var co = vd.GetContentObjectById(pid, false);
                var url = vd.GetContentUrl(co.PID, co.Location);
                vd.IncrementDownloads(pid);
                Website.Documents.ServeDocument(url, co.Location);

                break;

            case "Delete":

                //TODO: Implement

                break;



        }


    }
}