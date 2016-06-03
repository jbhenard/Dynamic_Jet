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
    public partial class FormEmployé : Form
    {
        public FormEmployé()
        {
            InitializeComponent();
        }

        public void SetTextBox1(string param1)
        {
            this.textBox1.Text = param1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Début code commun Ajout & Modifier            
            Employé d = new Employé(); //Class Data avec méthodes car class importer sur même NameSpace
            d.SetNumero_secu(textBox1.Text.Trim()); //nnchar(15) numero secu
            d.SetDate_visite_medicale(DateTime.Parse(textBox2.Text)); //date
            d.SetType_contrat(textBox3.Text.Trim()); //nchar(10) type contrat CDI, CDD, ...
            d.SetNumero_permis(textBox4.Text.Trim()); //nchar(50) permis
            d.SetStatut_activite(textBox5.Text.Trim()); //nchar(10) statut acitvité Permis
            d.SetNom(textBox6.Text.Trim()); //nchar(10) nom 
            d.SetPrenom(textBox7.Text.Trim()); //nhar(10) prenom
            d.SetDate_embauche(DateTime.Parse(textBox8.Text)); //date

            if (this.button1.Text == "&Ajout")
            {
                SqlDataAdapter sda = d.AfficherTous(d.Requete("Ajouter"));
            }
            else
            {
                SqlDataAdapter sda = d.AfficherTous(d.Requete("Modifier"));
            }
            d.deconnecter(); //Fin connexion
            //MessageBox.Show("Enregistrement crée !!! ", "Saisie OK", MessageBoxButtons.OK);

            //Form2.ActiveForm.Close();                

            //Fin code
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //Début code
            if (this.Text == "Ajout Employé(s)") this.button1.Text = "&Ajout";
            else
            {
                this.button1.Text = "&Modifier";

                if (String.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Saisir un identifant Employé à modifier !!! ", "Erreur de saisie", MessageBoxButtons.OK);
                }
                else
                {
                    Employé d = new Employé(); //Class Data avec méthodes car class importer sur même NameSpace                    
                    d.SetNumero_secu(textBox1.Text);
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
                    textBox8.Text = table.Rows[0][7].ToString(); //Alimenter le champs Mail avec resultat select sur id passé en paramètre
                    d.deconnecter(); //Fin connexion   
                }//Fin Si2
            } //Fin Si1
            //Fin code
        }
    }

}
