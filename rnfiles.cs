using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace RenametoDate
{
    class rnfiles
    {
        //public FileInfo rfile { get; set; }
        private string label { get; set; } = "";
		private string folder{ get; set; } = "";
        
        private bool front { get; set; } = false;
        private bool crtime { get; set; } = false;
        private List<string> ffn { get; set; } = new List<string>() { { "default" } };
        private List<string> nfn { get; set; } = new List<string>() { { "default" } };

        public rnfiles(string text, bool pos, bool time)
        {

            label = text;
            front = pos;
            crtime = time;
            
        }

		public bool rename()
		{
			int i,j;
            string newname;
            
			if(MessageBox.Show("ファイル名を変更してよろしいですか？","確認",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2) == DialogResult.Cancel) return false;

            for (i = 0; i < nfn.Count; i++) { 
                for(j=1; j < nfn.Count; j++)
                {
                    newname= nfn[i].Insert(nfn[i].LastIndexOf("."), "_" + j.ToString("D7"));
                    if (!File.Exists(this.folder + newname)) {
                        nfn[i] = newname;
                        break;
                    }
                }
                
                File.Move(folder + ffn[i], folder + nfn[i]); }
			return true;
		}


        public bool setdata(string[] files)
        {
            int posdr(string s) => s.LastIndexOf(@"\") + 1;
            if (files.Length <= 1) return false;
			if (posdr(files[1])<0)return false;


            this.folder =files[1].Substring(0,posdr(files[1]));

            this.ffn.Clear();
            this.nfn.Clear();
            

            FileInfo rfile;
            string name;
            name = "";
           
           

            int i;
            
            for (i = 1; i < files.Length; i++)
            {
                rfile = new FileInfo(files[i]);
                
                name = crtime ? rfile.CreationTime.ToString() : rfile.LastWriteTime.ToString();
                if (name.IndexOf(':') - name.IndexOf(' ') == 2) name = name.Insert(name.IndexOf(' ') + 1, "0");
                name = name.Replace("/", "");
                name = name.Replace(":", "");
                name = name.Replace(" ", "-");
                if (this.label != "")name = this.front ? this.label + name :name + this.label;
                
                name += rfile.Extension;


                ffn.Add(rfile.Name);
                nfn.Add(name);
 


            }
            // デバッグ用 /* 
            //MessageBox.Show(name, "filename", MessageBoxButtons.OK);

            // */

            return true;
        }

    }
}
