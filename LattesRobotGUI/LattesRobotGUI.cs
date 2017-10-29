using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lattesrobot;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using Newtonsoft.Json;
using LattesRobotGUI.lattesrobot;

namespace LattesRobotGUI
{
    /// <summary>
    /// LattesRobotGUI extends Form (Partial class)
    /// WinForm Window
    /// @author: Renan Medina
    /// </summary>
    public partial class LattesRobotGUI : Form
    {
        // bg_worker for backgrond thread execution 
        BackgroundWorker bg_worker = new BackgroundWorker();
        // Input filename when "File" type selected
        String input_filename;
        // create the robot instance and pass the Worker instance to ReportProgress Correctly
        LattesRobot robot;
        Timer totalTimer = new Timer();
        int totalTimeSecs = 0;

        /// <summary>
        /// LattesRobotGUI
        /// Constructor method for WinForm, initialize basic components for execution
        /// @author: Renan Medina
        /// </summary>
        public LattesRobotGUI()
        {
            // initialize Base::Components for WinForm
            InitializeComponent();
            this.totalTimer.Tick += TotalTimer_Tick;
            this.totalTimer.Interval = 1000; // 1s
            // initialize background worker configs
            this.bg_worker.WorkerReportsProgress = true;
            // add event Handler for DoWork event
            this.bg_worker.DoWork += Bg_worker_DoWork;
            // add event Handler for ProgressChanged event
            this.bg_worker.ProgressChanged += Bg_worker_ProgressChanged;
            // permit background worker to be cancelled
            this.bg_worker.WorkerSupportsCancellation = true;
            // initialize robot instance and pass Worker instance
            this.robot = new LattesRobot(this.bg_worker);
            // set initial records limit search index (25 records)
            cbLimitSearch.SelectedIndex = 0;
        }

        private void TotalTimer_Tick(object sender, EventArgs e)
        {
            this.totalTimeSecs++;
            this.lblTimeSpent.Text = this.totalTimeSecs.ToString() + "s";
        }

        /// <summary>
        /// Bg_worder_ProgressChanged
        /// Method handler for event ProgressChanged for background worker named as bg_worker
        /// @author: Renan Medina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bg_worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // get robot instance sent by "ReportProgress"
            LattesRobot _rb = (LattesRobot)e.UserState;
            // remove ID from download list
            this.listboxLattesIDS.Items.Remove(this.listboxLattesIDS.SelectedItem);
            // check percentage to display downloading next ID or just reset text
            if (e.ProgressPercentage < 100) {
                this.listboxLattesIDS.SelectedItem = _rb.getNextID();
                this.lblProgress.Text = String.Format("Baixando curriculo {0} ...", this.listboxLattesIDS.SelectedItem.ToString());
            }
            else
                this.lblProgress.Text = "";
            // set ProgressBar Percentage
            this.lblPercentage.Text = e.ProgressPercentage.ToString() + "%";
            this.pgrDownloads.Value = e.ProgressPercentage;

            // check if user asked the robot stop downloading.
            if (this.bg_worker.CancellationPending || e.ProgressPercentage >= 100)
            {
                this.robot.StopDownloading();
                this.totalTimer.Stop();
                this.btnStopRobot.Enabled = false;
                this.btnStartRobot.Enabled = true;
                if (e.ProgressPercentage >= 100){
                    this.robot.resetRobot();
                    Timer t = new Timer();
                    t.Interval = 4000;
                    EventHandler handler = new EventHandler(delegate (object s, EventArgs ev){
                        this.pgrDownloads.Value = 0;
                        this.lblProgress.Text = "";
                        this.lblPercentage.Text = "";
                        this.lblTimeSpent.Text = "0s";
                        t.Stop();
                        t.Dispose();
                    });
                    t.Tick += handler;
                    t.Start();
                }
                
            }
        }


        /// <summary>
        /// Bg_worker_DoWork
        /// Do work in background preventing lock the GUI thread.
        /// @author: Renan Medina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bg_worker_DoWork(object sender, DoWorkEventArgs e)
        {
            // set robot configs based on GUI configs
            this.robot.OutputFolder = this.txtOutputPath.Text;
            this.robot.OutputXML = this.rdXmlOutput.Checked;
            // check for null or empty separator char and display an error message
            if (string.IsNullOrWhiteSpace(this.txtSeparatorChar.Text))
                MessageBox.Show("Você selecionou a opção 'separação por caracter', informe o caracter se separação dos ID's.", "Erro de separação dos ID's lattes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else{
                if (!this.robot.isStopped){
                    List<String> ids = this.listboxLattesIDS.Items.Cast<String>().ToList<String>();
                    // initialize robot execution from list on listboxLattesIDS
                    this.robot.startFromList(ids);
                }
                else
                    this.robot.continueDownloads();
            }

        }

        private void setRobotSeparator()
        {
            if (this.rdSeparatorChar.Checked)
            {
                this.robot.SeparatorString = this.txtSeparatorChar.Text.ToString();
                this.txtSeparatorChar.ReadOnly = false;
                this.txtSeparatorChar.Text = !string.IsNullOrWhiteSpace(this.txtSeparatorChar.Text) ? this.txtSeparatorChar.Text : ",";
            }
            else if (this.rdSeparatorLine.Checked)
            {
                this.robot.SeparatorString = "\n";
                this.txtSeparatorChar.ReadOnly = true;
            }

            // force text change event to parse the ID's again
            txtIdsList_TextChanged(this.txtIdsList, new EventArgs());
        }



        private void btnOutputFolder_Click(object sender, EventArgs e)
        {
            DialogResult result = this.folderOuputDialog.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(this.folderOuputDialog.SelectedPath))
            {
                this.txtOutputPath.Text = this.folderOuputDialog.SelectedPath;
            }
        }

        private void btnStartRobot_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtOutputPath.Text))
                MessageBox.Show("Selecione a pasta de saída dos curriculos.", "Erro de saída dos curriculos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (this.listboxLattesIDS.Items.Count == 0)
                MessageBox.Show("Informe pelo menos um lattes_id para realizar o download.", "Erro de ID lattes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (this.listboxLattesIDS.SelectedItem == null)
                    this.listboxLattesIDS.SelectedIndex = 0;              

                this.lblProgress.Text = String.Format("Baixando curriculo {0} ...", this.listboxLattesIDS.SelectedItem.ToString());
                this.btnStopRobot.Enabled = true;
                this.btnStartRobot.Enabled = false;
                this.totalTimer.Start();
                this.bg_worker.RunWorkerAsync();
            }
        }

        private void rdFileInput_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdFileInput.Checked)
            {
                DialogResult rst = this.openFileInputDialog.ShowDialog();
                if (rst == DialogResult.OK && !string.IsNullOrWhiteSpace(this.openFileInputDialog.FileName))
                {
                    this.input_filename = this.openFileInputDialog.FileName;
                    this.lblSelectedFile.Text = this.input_filename;
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(this.input_filename))
                    {
                        this.txtIdsList.Text = reader.ReadToEnd();
                    }
                }
                else
                {
                    this.input_filename = null;
                    this.rdTxtInput.Checked = true;
                }
            }
        }

        private void rdTxtInput_CheckedChanged(object sender, EventArgs e)
        {
            this.input_filename = null;
            this.lblSelectedFile.Text = "Nenhum arquivo selecionado";
        }

        private void txtIdsList_TextChanged(object sender, EventArgs e)
        {
            this.listboxLattesIDS.Items.Clear();
            List<String> ids = this.robot.parseIds(this.txtIdsList.Text);
            this.listboxLattesIDS.Items.AddRange(ids.ToArray<String>());
            this.listboxLattesIDS.SelectedIndex = 0;
            this.setIDsQuantity();
        }

        private void rdSeparatorLine_CheckedChanged(object sender, EventArgs e)
        {
            setRobotSeparator();
        }

        private void rdSeparatorChar_CheckedChanged(object sender, EventArgs e)
        {
            setRobotSeparator();
        }

        private void txtSeparatorChar_TextChanged(object sender, EventArgs e)
        {
            setRobotSeparator();
        }

        private void btnSearchLattes_Click(object sender, EventArgs e)
        {
            prepareSearch();
            Timer interval_effect = new Timer();
            interval_effect.Interval = 1000;
            interval_effect.Tick += Interval_effect_Tick;
            interval_effect.Start();
            // searchLattesPersons();
        }

        private void Interval_effect_Tick(object sender, EventArgs e)
        {
            Timer t = (Timer)sender;
            t.Stop();
            t.Dispose();
            searchLattesPersons();
        }

        private void searchLattesPersons()
        {
            String name = txtSearchName.Text.ToString();
            int limit = int.Parse(cbLimitSearch.SelectedItem.ToString());

            HttpWebRequest xhr = (HttpWebRequest)WebRequest.Create(LattesPersonResult.SEARCH_BASE_URL);

            string json = "{\"filtros\":[{\"label\":\"idx_nme_pessoa_t\"," +
                              "\"normalizador\":\"sufixoParcial\"," +
                              "\"conteudo\":\"" + name + "\"}]," +
                           "\"paginacao\":{\"start\":0, \"rows\":\"" + limit.ToString() + "\"}}";

            xhr.Method = "POST";
            xhr.ContentType = "application/json";
            xhr.MediaType = "application/json";

            using (var streamWriter = new StreamWriter(xhr.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            try
            {
                HttpWebResponse response = (HttpWebResponse)xhr.GetResponse();
                String responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                response.Close();
                dynamic result_json = JsonConvert.DeserializeObject(responseString);
                var results = result_json[0]["docs"];
                int total_found = (int)result_json[0]["numFounds"];
                lblSearching.Visible = false;

                if (results == null)
                {
                    MessageBox.Show("Nenhum resultado encontrado para: " + name, "Não encontrado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSearchName.Text = "";
                    btnDownloadSelects.Enabled = false;
                    return;
                }

                btnDownloadSelects.Enabled = true;
                lnkResultCount.Text = "exibindo " + results.Count.ToString() + " de " + total_found.ToString();
                lnkResultCount.Visible = true;

                foreach (var doc in results)
                {

                    LattesPersonResult per = new LattesPersonResult(
                        (String)doc["idx_nme_pessoa_t"],
                        (String)doc["additionalProperties"]["graduacao_maxima_s"],
                        (String)doc["cod_rh_cript_s"],
                        (String)doc["additionalProperties"]["nro_id_cnpq_s"]
                    );
                    dataGridResultSearch.Rows.Add(per.CNPQId, per.RHCode, per.Name, per.Education);
                }
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show("Não foi possivel realizar a pesquisa, favor verifique sua internet ou tente novamente mais tarde.", "Problema na pesquisa", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void prepareSearch()
        {
            dataGridResultSearch.Rows.Clear();
            lblSearching.Visible = true;
        }

        private void btnDownloadSelects_Click(object sender, EventArgs e)
        {
            // retrieve selected rows collection from datagrid
            DataGridViewSelectedRowCollection rows = dataGridResultSearch.SelectedRows;
            // check if there isn't any row selected
            if(rows.Count == 0){
                MessageBox.Show("Favor selecione algum curriculo para adicionar à fila de downloads.", "Nenhum curriculo selecionado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // foreach row selected add the LattesID to download list
            foreach (DataGridViewRow r in rows)
            {
                String id = (String) r.Cells[0].Value;
                if (this.listboxLattesIDS.Items.IndexOf(id) == -1)
                    this.listboxLattesIDS.Items.Add(id);
            }

            // move to first tab (Robot)
            tabControlMain.SelectedIndex = 0;
            this.setIDsQuantity();
            // displays success message
            //MessageBox.Show("Códigos lattes selecionados foram adicionados à fila de download", "Adicionar à fila de downloads", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void setIDsQuantity()
        {
            this.lblIdsCount.Text = this.listboxLattesIDS.Items.Count.ToString();
        }

        /// <summary>
        /// CellDoubleClick event handler for DataGridView named as dataGridResultSearch
        /// it opens a web browser with the curriculum lattes for the row double clicked
        /// </summary>
        /// <param name="sender">Object that sent the event ([DataGridView] dataGridResultSearch)</param>
        /// <param name="e">Event arguments sent after double click event ([DataGridViewCellEventArgs])</param>
        private void dataGridResultSearch_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // get double clicked row
            DataGridViewRow dblRow = dataGridResultSearch.Rows[e.RowIndex];
            // open web browser to see profile
            System.Diagnostics.Process.Start(LattesPersonResult.getProfileURL(dblRow.Cells[0].Value.ToString()));
        }

        /// <summary>
        /// KeyDown event handler for Textbox named as txtSearchName
        /// it will trigger the search button when ENTER key is pressed
        /// </summary>
        /// <param name="sender">Object that sent the event ([TextBox] txtSearchName)</param>
        /// <param name="e">Event arguments sent after key down ([KeyEventArgs])</param>
        private void txtSearchName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSearchLattes.PerformClick();
                return;
            }
        }

        /// <summary>
        /// btnStopRobot_Click
        /// Method handler for btnStopRobot click event
        /// @author: Renan Medina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopRobot_Click(object sender, EventArgs e)
        {
            this.bg_worker.CancelAsync();
        }

        private void cbLimitSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnSearchLattes.PerformClick();
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtOutputPath.Text))
                MessageBox.Show("Selecione uma pasta de saída para abrir", "Pasta de saída", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                System.Diagnostics.Process.Start(this.txtOutputPath.Text);
        }
    }
} 
