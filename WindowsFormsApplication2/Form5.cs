using Base_Dynamic_Jet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        //Box avec id absence
        public void SetTextBox4(string param1)
        {
            this.textBox4.Text = param1;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            //Début code
            if (this.Text == "Ajout Absence(s)") this.button1.Text = "&Ajout";
            else
            {
                this.button1.Text = "&Modifier";

                if (String.IsNullOrEmpty(textBox4.Text)) //Id absence en posiiton 4 des textbox
                {
                    MessageBox.Show("Saisir une Absence à modifier !!! ", "Erreur de saisie", MessageBoxButtons.OK);
                }
                else
                {
                    Absence d = new Absence(); //Class Data avec méthodes car class importer sur même NameSpace
                    d.SetId_absence(textBox4.Text);
                    SqlDataAdapter sda = d.AfficherTous(d.Requete("FD"));
                    DataTable table = new DataTable();  //DataSet
                    sda.Fill(table);
                    textBox1.Text = table.Rows[0][0].ToString(); //Alimenter le champs id avec la valeur passé en paramètre
                    textBox2.Text = table.Rows[0][1].ToString(); //Alimenter le champs Nom avec resultat select sur id passé en paramètre
                    textBox3.Text = table.Rows[0][2].ToString(); //Alimenter le champs Prénom avec resultat select sur id passé en paramètre
                    //textBox4.Text = table.Rows[0][3].ToString(); //Alimenter le champs Adresse avec resultat select sur id passé en paramètre
                    textBox5.Text = table.Rows[0][4].ToString(); //Alimenter le champs Téléphone avec resultat select sur id passé en paramètre                    
                    d.deconnecter(); //Fin connexion   
                }//Fin Si2
            } //Fin Si1
            //Fin code
        }

        //Ajouter Absence(s)
        private void button1_Click(object sender, EventArgs e)
        {
            //Début code commun Ajout & Modifier            
            Absence d = new Absence(); //Class Data avec méthodes car class importer sur même NameSpace
            d.SetId_employe(textBox1.Text); //string id employé
            d.SetDate_debut(DateTime.Parse(textBox2.Text)); //Date date début
            d.SetDate_fin(DateTime.Parse(textBox3.Text)); //Date date fin
            d.SetId_absence(textBox4.Text); //nvarchar(255) id absence
            d.SetMotif(textBox5.Text); //nvarchar(255) motif            

            if (this.button1.Text == "&Ajout")
            {
                SqlDataAdapter sda = d.AfficherTous(d.Requete("Ajouter"));
            }
            else
            {
                SqlDataAdapter sda = d.AfficherTous(d.Requete("Modifier"));
            }
            d.deconnecter(); //Fin connexion            
            //Fin code
        }
    }
}
