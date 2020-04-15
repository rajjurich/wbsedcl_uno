using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

namespace UNO
{
    //Added by Pooja Yadav as on 19/12/2014
    public partial class DirectoryPopup : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                GetAllDirectories();
            }
            TrDirectoryView.SelectedNodeChanged += new EventHandler(TrDirectoryView_SelectedNodeChanged);
            TrDirectoryView.TreeNodeExpanded += new TreeNodeEventHandler(TrDirectoryView_TreeNodeExpanded);
        }
        public TreeNode OutputDirectory(System.IO.DirectoryInfo directory, TreeNode parentNode)
        {
            if (directory == null) return null;
            // create a node for this directory
            TreeNode DirNode = new TreeNode(directory.Name);
            // get subdirectories of the current directory
            System.IO.DirectoryInfo[] SubDirectories = null;
            TreeNode tree = TrDirectoryView.FindNode(directory.Name);
            if (tree == null)
            {
                try
                {
                    SubDirectories = directory.GetDirectories();
                }
                catch (Exception e)
                {
                }

                if (SubDirectories != null)
                {
                    for (int DirectoryCount = 0; DirectoryCount < SubDirectories.Length; DirectoryCount++)
                        OutputDirectory(SubDirectories[DirectoryCount], DirNode);

                }

            }
            if (parentNode == null)
            {
                return DirNode;
            }
            else
            {
                if (!(DirNode.ChildNodes.Count > 0))
                    DirNode.ImageUrl = "~/Images/Folder.jpg";

                parentNode.ChildNodes.Add(DirNode);
                return parentNode;
            }

        }

        protected void TrDirectoryView_SelectedNodeChanged(object sender, EventArgs e)
        {
            _browseTextBox.Text = TrDirectoryView.SelectedValue;
        }
        void TrDirectoryView_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
        {
            if (e.Node.Value.EndsWith("\\"))
            {
                TreeNodeCollection trcol = TrDirectoryView.Nodes;
                var tree = FindNodeByValue(TrDirectoryView.Nodes, e.Node.Value).FirstOrDefault();
                if (tree != null)
                {
                    if (tree.ChildNodes.Count > 0)
                    { }
                    else
                    {
                        FileList objList = new FileList(e.Node.Value, "*.*");
                        if (objList.Directories != null)
                            if (objList.Directories.Length != 0)
                                AddNodes(e.Node.Value, e.Node);
                            else
                                e.Node.ImageUrl = "~/Images/Folder.jpg";
                    }
                }
                else
                    AddNodes(e.Node.Value, e.Node);
            }

        }
        private void GetAllDirectories()
        {
            hdnPathTextBoxId.Value = Request.QueryString["txtSQLBackupPathID"].ToString();
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                TreeNode onjParent = new TreeNode(drive.ToString(), drive.ToString());
                onjParent.PopulateOnDemand = true;
                TrDirectoryView.Nodes.Add(onjParent);

            }
      
            TrDirectoryView.CollapseAll();            
        }
        private TreeNode AddNodes(string path, TreeNode parentNode)
        {

            FileList objList = new FileList(path, "*.*");
            TreeNode node = new TreeNode(path, path);
            for (int index = 0; index < objList.Directories.Length; index++)
            {
                string directory = objList.Directories[index];
                if (directory != path)
                {
                    TreeNode objChildNode = new TreeNode(directory, path + "\\" + directory + "\\");
                    objChildNode.PopulateOnDemand = true;
                    objChildNode.Target = "_blank";
                    objChildNode.ImageUrl = "~/Images/Folder.jpg";
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path + "\\" + directory + "\\");
                    if ((dir.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                        parentNode.ChildNodes.Add(objChildNode);
                }
                else
                    node = null;
            }

            return node;

        }
        private IEnumerable<TreeNode> FindNodeByValue(TreeNodeCollection nodes, string searchstring)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Value.IndexOf(searchstring,
                      StringComparison.CurrentCultureIgnoreCase) >= 0)
                    yield return node;
                else
                {
                    foreach (var subNode in FindNodeByValue(node.ChildNodes, searchstring))
                        yield return subNode;
                }
            }
        }
        public static string GetUNCPath(string originalPath)
        {
            StringBuilder sb = new StringBuilder(512);
            int size = sb.Capacity;
            // look for the {LETTER}: combination ...
            if (originalPath.Length > 2 && originalPath[1] == ':')
            {
                // don't use char.IsLetter here - as that can be misleading
                // the only valid drive letters are a-z && A-Z.
                char c = originalPath[0];
                if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
                {
                    //int error = WNetGetConnection(originalPath.Substring(0, 2),
                    //    sb, ref size);
                    int error = 0;
                    if (error == 0)
                    {
                        DirectoryInfo dir = new DirectoryInfo(originalPath);
                        string path = Path.GetFullPath(originalPath)
                            .Substring(Path.GetPathRoot(originalPath).Length);
                        return Path.Combine(sb.ToString().TrimEnd(), path);
                    }
                }
            }
            return originalPath;
        }




    }
}