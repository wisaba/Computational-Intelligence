namespace XO_Perceptron
{
    public partial class Form1 : Form
    {
        public static double alpha = 0.01, theta = 0;
        public static double[] values = new double[25];
        public static double[] weights = new double[26];
        static string filePath_dataset = "C:/Users/Ali/Desktop/ci/XO_Perceptron/XO_Perceptron/datasets.txt";
        public Form1()
        {
            InitializeComponent();
            InitializeButtonEvents();
        }
        public class buttons_information
        {
            public Button button { get; set; }
            public Color color { get; set; }
        }
        private void InitializeButtonEvents()
        {
            for (int i = 1; i <= 25; i++)
            {
                Button button = (Button)this.Controls.Find($"button{i}", true)[0];
                button.BackColor = Color.White;
                button.Click += Button_Click;
            }
        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                if (clickedButton.BackColor == Color.White)
                    clickedButton.BackColor = Color.Black;
                else
                    clickedButton.BackColor = Color.White;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void CheckerButton_Click(object sender, EventArgs e)
        {
            
            ChangeWeigths();
            buttons_information[] buttonsArray = new buttons_information[25];
            for (int i = 1; i <= 25; i++)
            {
                Button button = (Button)this.Controls.Find($"button{i}", true)[0];
                buttonsArray[i - 1] = new buttons_information
                {
                    button = button,
                    color = button.BackColor
                };
            }
            for (int i = 0; i < values.Length; i++)
            {
                if (buttonsArray[i].color != Color.White)
                    values[i] = 1;
                else
                    values[i] = -1;
            }
            foreach (var buttonInfo in buttonsArray)
            {
                buttonInfo.button.BackColor = Color.White;
            }
            double sum = 0;
            for (int i = 0; i < 25; i++)
            {
                sum = sum + (values[i] * weights[i]);
            }
            sum = sum + (weights[25]);
            if (sum >= 0)
                ResultLabel.Text = "Result Is : X";
            else
                ResultLabel.Text = "Result Is : O";
        }

        private void TrainButton_Click(object sender, EventArgs e)
        {
            buttons_information[] buttonsArray = new buttons_information[25];
            for (int i = 1; i <= 25; i++)
            {
                Button button = (Button)this.Controls.Find($"button{i}", true)[0];
                buttonsArray[i - 1] = new buttons_information
                {
                    button = button,
                    color = button.BackColor
                };
            }
            for (int j = 0; j < 25; j++)
            {
                if (buttonsArray[j].color == Color.Black)
                {
                    values[j] = 1;
                }
                else
                {
                    values[j] = -1;
                }
            }
            foreach (var buttonInfo in buttonsArray)
            {
                buttonInfo.button.BackColor = Color.White;
            }
            string Target = Selector.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(Target))
            {
                TrainLabel.Text = "Trained As " + Target;
                initilize_weights();
                SaveToDataset();
            }
            else
            {
                TrainLabel.Text = "Please Try Again!";
            }

        }
        static int FYNI(double YNI)
        {
            if (YNI > theta)
            {
                return 1;
            }
            else if (YNI < -1 * theta)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        private void ChangeWeigths()
        {
            string[] lines = File.ReadAllLines(filePath_dataset);
            for (int i = 0; i < lines.Length; i++)
            {
                
                string[] values_string = lines[i].Split('.');
                double[] valuess = new double[values_string.Length];
                for (int k = 0; k < 26; k++)
                {
                    valuess[k] = int.Parse(values_string[k]);
                }
                int y = Convert.ToInt32(valuess[25]);





                again:
                double sum = 0;
                for (int t = 0; t < 25; t++)
                {
                    sum = sum + (valuess[i] * weights[i]);
                }
                sum = sum + (weights[25]);
                int check_stop = FYNI(sum);
                while(check_stop != y)
                {
                    double[] delta_weights = new double[26];
                    for (int j = 0; j < 25; j++)
                    {
                        delta_weights[j] = valuess[j] * y * alpha;
                    }
                    delta_weights[25] = y * alpha;

                    for (int j = 0; j < 26; j++)
                        weights[j] = delta_weights[j] + weights[j];
                    goto again;
                }
                
            }

        }

        private void initilize_weights()
        {
            for (int i = 0; i < 25; i++)
            {
                weights[i] = 0;
            }
        }

        private void SaveToDataset()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath_dataset, true))
                {
                    writer.Write(string.Join(".", values));
                    string y = Selector.Text.ToString();
                    writer.Write(".");
                    if (y.Equals("X"))
                    {
                        writer.Write("1");
                    }
                    else
                    {
                        writer.Write("-1");
                    }
                    writer.Write("\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

    }
}