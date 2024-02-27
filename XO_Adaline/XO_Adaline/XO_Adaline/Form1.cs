namespace XO_Adaline
{
    public partial class Form1 : Form
    {
        public static int[] values = new int[25];
        public static double[] weights = new double[26];
        public static double alpha = 0.01;
        public static double theta = 0;
        static string filePath_dataset = "C:/Users/IP GAMING 3-15IAH7/Desktop/Computational Intelligence/XO_Adaline/XO_Adaline/XO_Adaline/datasets.txt";
        public Form1()
        {
            InitializeComponent();
            InitializeButtonEvents();
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
        public class buttons_information
        {
            public Button button { get; set; }
            public Color color { get; set; }
        }
        private void initilize_weights()
        {
            for (int i = 0; i < 25; i++)
            {
                weights[i] = 0;
            }
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
        static double YNI(int[] outed, double[] old_weights)
        {
            double sum = 0;
            for (int i = 0; i < 25; i++)
            {
                sum = sum + ((double)outed[i] * old_weights[i]);
            }
            sum += old_weights[25];
            return sum;
        }
        private void ChangeWeigths()
        {
            string[] lines = File.ReadAllLines(filePath_dataset);
            for (int i = 0; i < lines.Length; i++)
            {
                string[] values_string = lines[i].Split('.');
                int[] valuess = new int[values_string.Length];
                for (int k = 0; k < 26; k++)
                {
                    valuess[k] = int.Parse(values_string[k]);
                }
                int y = Convert.ToInt32(valuess[25]);
                double[] delta_weights = new double[26];
                double stop_condition = 0.001;//mamoolan beyn 0.001 ta 0.000001
                bool flag = true;
                while (flag)
                {
                    double y_input = YNI(valuess, weights);
                    for(int z = 0; z < 25; z++)
                    {
                        delta_weights[z] = alpha * (y - y_input) * valuess[z];
                    }
                    delta_weights[25] = alpha * (y - y_input);
                    for(int z = 0;z<26; z++)
                    {
                        weights[z] = weights [z] + delta_weights[z];
                    }
                    double maximum_delta = Math.Abs(delta_weights.Max());
                    if(maximum_delta < stop_condition)
                        flag = false;
                }
            }

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
            int step = 0;
            if (sum >= 0)
                step = 1;
            else
                step = -1;
            if (step == 1)
            {
                ResultLabel.Text = "It's X!";
            }
            else
            {
                ResultLabel.Text = "It's O!";
            }
        }
    }
}