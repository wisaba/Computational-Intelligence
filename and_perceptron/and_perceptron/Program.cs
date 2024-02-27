class and_perceptron
{
    
    static double theta = 0.25 , y, alpha = 1,f;
    static double[] weights = new double[3];//w1,w2,wb
    static double[] signals = new double[2];
    static double[,] and_table = { { 1, 1, 1 }, { 1, -1, -1 }, { -1, 1, -1 }, { -1, -1, -1 } };
    

    public static void Main(String[] args)
    {
        for(int i = 0; i < 3; i++)
        {
            weights[i] = 0;
        }
        Console.WriteLine("enter the inputs");
        signals[0] = Convert.ToInt32(Console.ReadLine());
        signals[1] = Convert.ToInt32(Console.ReadLine());
        and_perceptron pr = new and_perceptron();
        for(int i = 0; i < 4; i++)
        {
            if (f != and_table[i, 2])
            {
                pr.train();
                pr.sigma();
                pr.activation_fun();
            }
                    
                    
        }
        Console.WriteLine("output : "+f);
    }
    public void sigma()
    {
        y = weights[2];
        for( int i = 0; i < 2; i++)
        {
            y+= weights[i]*signals[i];
        }

        
    }
    public void activation_fun()
    {
        if (y< (-1 * theta))
        {
           f = -1 ;
        }
        else if (y > theta)
            f= 1;
        else 
            f = 0;
    }

    public void train()
    {
        for( int i = 0; i <4; i++)
        {
            weights[0] += and_table[i,0] * and_table[i,2] * alpha;
            weights[1] += and_table[i,1] * and_table[i,2] * alpha;
            weights[2] = alpha * and_table[i,2];

        }
    }
}
