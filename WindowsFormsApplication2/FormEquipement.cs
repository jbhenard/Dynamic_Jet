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
    public partial class FormEquipement : Form
    {
        public FormEquipement()
        {
            InitializeComponent();
        }

        //Setter pour passage info du formulaire Equipement à ce formualire
        public void SetTextBox1(string param1)
        {
            this.textBox1.Text = param1;
        }
        private void Form7_Load(object sender, EventArgs e)
        {
            //Début code
            if (this.Text == "Ajout Equipement(s)") this.button1.Text = "&Ajout";
            else
            {
                this.button1.Text = "&Modifier";

                if (String.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Saisir un Equipement à modifier !!! ", "Erreur de saisie", MessageBoxButtons.OK);
                }
                else
                {
                    Equipement d = new Equipement(); //Class Data avec méthodes car class importer sur même NameSpace
                    d.SetId_equipement(Int32.Parse(textBox1.Text));
                    SqlDataAdapter sda = d.AfficherTous(d.Requete("FD"));
                    DataTable table = new DataTable();  //DataSet
                    sda.Fill(table);
                    //textBox1.Text = table.Rows[0][0].ToString(); //Alimenter le champs id avec la valeur passé en paramètre
                    textBox2.Text = table.Rows[0][1].ToString(); //Alimenter le champs Nom avec resultat select sur id passé en paramètre
                    textBox3.Text = table.Rows[0][2].ToString(); //Alimenter le champs Prénom avec resultat select sur id passé en paramètre
                    textBox4.Text = table.Rows[0][3].ToString(); //Alimenter le champs Adresse avec resultat select sur id passé en paramètre
                    textBox5.Text = table.Rows[0][4].ToString(); //Alimenter le champs Téléphone avec resultat select sur id passé en paramètre
                    textBox6.Text = table.Rows[0][5].ToString(); //Alimenter le champs Date de naissance avec resultat select sur id passé en paramètre
                    textBox7.Text = table.Rows[0][6].ToString(); //Alimenter le champs N° de permis avec resultat select sur id passé en paramètre                   
                    d.deconnecter(); //Fin connexion   
                }//Fin Si2
            } //Fin Si1
            //Fin code

        }

        /// <summary>
        /// Ajout - modifier Equipement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //Début code commun Ajout & Modifier            
            Equipement d = new Equipement(); //Class Data avec méthodes car class importer sur même NameSpace
            d.SetId_equipement(Int32.Parse(textBox1.Text)); //int idclient
            d.SetNom(textBox2.Text.Trim()); ////nchar(15) nom
            d.SetDescriptif(textBox3.Text.Trim()); ////nchar(15) prenom
            d.SetPuissance(Int32.Parse(textBox4.Text.Trim())); ////nchar(50) adresse
            d.SetEtat(textBox5.Text.Trim()); ////nchar(20) telephone
            d.SetPrix_ht(float.Parse(textBox6.Text.Trim())); ////Date naissance
            d.SetId_employe(textBox7.Text.Trim()); ////nchar(50) permis            

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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
