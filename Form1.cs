using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Database;
using Firebase.Database.Http;
using FirebaseCoreSDK;

class Neuron
{
    private double[] weights;
    private double bias;
    public Neuron(int inputCount)
    {
        weights = new double[inputCount];
        bias = 0;
    }
    double sum;
    public void training(double input1,double input2,double input3,double output, double learning, int looplenght)
    {

        for (int i = 0; i < looplenght; i++)
        {
            double summ = (input1 * weights[0]) + (input2 * weights[1]) + (input3 * weights[2]);
            double realoutput = Sigmoid(summ);
            double error = output- realoutput;


            weights[0] += (learning * error * input1);
            weights[1] += (learning * error * input2);
            weights[2] += (learning * error * input3);
            bias += learning * error;
        }
    }

    public void weightsset(params double[] value)
    {
        foreach (int i in value)
        {
            if(i>weights.Length)
            {
                break;
            }
            weights[i] = value[i];
        }

    }

    public double Activate(double[] inputs)
    {
        if (inputs.Length != weights.Length)
        {
            throw new ArgumentException("Number of inputs must match number of weights.");
        }

        double weightedSum = bias;
        for (int i = 0; i < inputs.Length; i++)
        {
            weightedSum += inputs[i] * weights[i];
        }

        double output = Sigmoid(weightedSum);
        return output;
    }

    private double Sigmoid(double x)
    {
        return 1 / (1 + Math.Exp(-x)); 
    }
}
namespace neuron
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            

            Neuron neuron = new Neuron(3);

            double[] inputs = { 0.5, 0.4, 0.8 };
            neuron.weightsset(0.3,0.3,0.2);
            neuron.training(0.2, 0.4, 0.3, 0.3, 120, 10000);
            double output = neuron.Activate(inputs);
            label1.Text = $"Output value: {output}";


        }
    }
    
}
