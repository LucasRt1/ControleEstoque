using System;
using System.Drawing;
using System.Windows.Forms;

namespace ControleEstoque
{
    public partial class Form1 : Form
    {
        // ==== DECLARAÇÕES DOS CONTROLES (COLOQUE NO INÍCIO DA CLASSE) ====
        private TextBox txtNome, txtPreco, txtQuantidade, txtId;
        private Button btnCadastrar, btnIncrementar, btnDecrementar;
        private DataGridView dataGridView1;
        private Label lblNome, lblPreco, lblQuantidade, lblId;

        ProdutoDAO dao = new ProdutoDAO();

        // ==== CONSTRUTOR ====
        public Form1()
        {
            InitializeComponent();
            this.Text = "Controle de Estoque";
            this.Size = new Size(500, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Chama a criação da interface
            CriarInterface();
        }

        // ==== MÉTODO PARA CRIAR A INTERFACE PROGRAMATICAMENTE ====
        private void CriarInterface()
        {
            int margemEsquerda = 20;
            int larguraLabel = 100;
            int larguraTextbox = 200;
            int altura = 25;
            int espacoVertical = 10;
            int y = 20;

            // Nome
            lblNome = new Label()
            {
                Text = "Nome:",
                Left = margemEsquerda,
                Top = y,
                Width = larguraLabel,
                Font = new Font("Arial", 10)
            };
            txtNome = new TextBox()
            {
                Left = margemEsquerda + larguraLabel,
                Top = y,
                Width = larguraTextbox,
                Font = new Font("Arial", 10)
            };
            this.Controls.Add(lblNome);
            this.Controls.Add(txtNome);
            y += altura + espacoVertical;

            // Preço
            lblPreco = new Label()
            {
                Text = "Preço:",
                Left = margemEsquerda,
                Top = y,
                Width = larguraLabel,
                Font = new Font("Arial", 10)
            };
            txtPreco = new TextBox()
            {
                Left = margemEsquerda + larguraLabel,
                Top = y,
                Width = larguraTextbox,
                Font = new Font("Arial", 10)
            };
            this.Controls.Add(lblPreco);
            this.Controls.Add(txtPreco);
            y += altura + espacoVertical;

            // Quantidade
            lblQuantidade = new Label()
            {
                Text = "Quantidade:",
                Left = margemEsquerda,
                Top = y,
                Width = larguraLabel,
                Font = new Font("Arial", 10)
            };
            txtQuantidade = new TextBox()
            {
                Left = margemEsquerda + larguraLabel,
                Top = y,
                Width = larguraTextbox,
                Font = new Font("Arial", 10)
            };
            this.Controls.Add(lblQuantidade);
            this.Controls.Add(txtQuantidade);
            y += altura + espacoVertical;

            // ID Produto
            lblId = new Label()
            {
                Text = "ID Produto:",
                Left = margemEsquerda,
                Top = y,
                Width = larguraLabel,
                Font = new Font("Arial", 10)
            };
            txtId = new TextBox()
            {
                Left = margemEsquerda + larguraLabel,
                Top = y,
                Width = larguraTextbox,
                Font = new Font("Arial", 10)
            };
            this.Controls.Add(lblId);
            this.Controls.Add(txtId);
            y += altura + espacoVertical * 2;

            // Botões
            btnCadastrar = new Button()
            {
                Text = "Cadastrar",
                Left = margemEsquerda,
                Top = y,
                Width = 100,
                Font = new Font("Arial", 10),
                BackColor = Color.LightBlue
            };
            btnCadastrar.Click += btnCadastrar_Click;

            btnIncrementar = new Button()
            {
                Text = "Incrementar",
                Left = margemEsquerda + 110,
                Top = y,
                Width = 100,
                Font = new Font("Arial", 10),
                BackColor = Color.LightGreen
            };
            btnIncrementar.Click += btnIncrementar_Click;

            btnDecrementar = new Button()
            {
                Text = "Decrementar",
                Left = margemEsquerda + 220,
                Top = y,
                Width = 100,
                Font = new Font("Arial", 10),
                BackColor = Color.LightCoral
            };
            btnDecrementar.Click += btnDecrementar_Click;

            this.Controls.Add(btnCadastrar);
            this.Controls.Add(btnIncrementar);
            this.Controls.Add(btnDecrementar);
            y += altura + espacoVertical * 2;

            // DataGridView
            dataGridView1 = new DataGridView()
            {
                Left = margemEsquerda,
                Top = y,
                Width = 450,
                Height = 200,
                Font = new Font("Arial", 10),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            this.Controls.Add(dataGridView1);
        }

        // ==== MÉTODOS DE EVENTOS E LÓGICA ====
        private void Form1_Load(object sender, EventArgs e)
        {
            AtualizarGrid();
        }

        private void AtualizarGrid()
        {
            try
            {
                dataGridView1.DataSource = ProdutoDAO.ListarProdutos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar produtos: " + ex.Message);
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                string nome = txtNome.Text;
                decimal preco = decimal.Parse(txtPreco.Text);
                int qtd = int.Parse(txtQuantidade.Text);

                // Verifica se produto já existe
                int idExistente = dao.VerificarProdutoExistente(nome);

                if (idExistente > 0)
                {
                    DialogResult resposta = MessageBox.Show(
                        $"O produto '{nome}' já existe (ID: {idExistente}). Deseja incrementar o estoque?",
                        "Produto Existente",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (resposta == DialogResult.Yes)
                    {
                        dao.IncrementarQuantidade(idExistente, qtd);
                        MessageBox.Show($"Estoque de '{nome}' incrementado em {qtd} unidades!");
                    }
                }
                else
                {
                    dao.CadastrarProduto(nome, preco, qtd);
                    MessageBox.Show("Produto cadastrado com sucesso!");
                }

                // Limpa os campos
                txtNome.Clear();
                txtPreco.Clear();
                txtQuantidade.Clear();
                AtualizarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar: " + ex.Message);
            }
        }

        private void btnIncrementar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtId.Text);
                int qtd = int.Parse(txtQuantidade.Text);

                dao.IncrementarQuantidade(id, qtd);
                MessageBox.Show("Quantidade incrementada!");

                txtId.Clear();
                txtQuantidade.Clear();
                AtualizarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao incrementar: " + ex.Message);
            }
        }

        private void btnDecrementar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtId.Text);
                int qtd = int.Parse(txtQuantidade.Text);

                dao.DecrementarQuantidade(id, qtd);
                MessageBox.Show("Quantidade decrementada!");

                txtId.Clear();
                txtQuantidade.Clear();
                AtualizarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao decrementar: " + ex.Message);
            }
        }
    }
}