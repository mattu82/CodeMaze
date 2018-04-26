using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodeMaze
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSolve_Click(object sender, EventArgs e)
        {
            var client = new MazeSolverService.MazeSolverServiceSoapClient("MazeSolverServiceSoap");
            txtMaze.Text = client.MazeSolver(txtMaze.Text);
        }
    }
}