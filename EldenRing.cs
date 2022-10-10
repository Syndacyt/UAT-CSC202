using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompletionistPrototype
{
    public partial class EldenRing : Form
    {
        public EldenRing()
        {
            InitializeComponent();
        }

        private void EldenRing_Load(object sender, EventArgs e)
        {
            // List of all the buttons containing an item
            List<Control> items = new List<Control> {  };

            foreach (Control c in Controls) 
            {
                if (c.GetType() == typeof(Button)) 
                {
                    if (c.Name == "item") 
                    {
                        items.Add(c);
                    }
                }
            }

            for (int i = 0; i < items.Count; i++)
            {
                
            }
        }
    }
}
