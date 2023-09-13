using System;

namespace FunMath
{
    public interface Item
    {
        int Count { get; set; }
        bool Empty() { return Count <= 0; }
        void Combine(Item other)
        {
            Count += other.Count;
        }
        bool Equals(Item other);
        string Name { get; }
    }

    public enum OperationType
    {
        // These are the type of items you can pick up, aka our math 'operators'
        Addition = 0,
        Subtraction,
        Multiply,
        Divide,
        OperationMax
    }

    public class OperationItem: Item
    {
        public OperationType Operator { get; private set; }
        public int Count { get; set; }

        public string Name { get => ConciseName();}

        private string ConciseName()
        {
            switch (Operator)
            {
                case OperationType.Addition:
                    return "+";
                case OperationType.Subtraction:
                    return "-";
                case OperationType.Multiply:
                    return "x";
                case OperationType.Divide:
                    return "÷";
            }
            return "";
        }

        public OperationItem(OperationType type, int count = 1)
        {
            Operator = type;
            Count = count;
        }

        public bool Equals(Item other)
        {
            if (other is OperationItem)
            {
                return Operator == ((OperationItem)other).Operator;
            }
            return false;
        }
    }

    public class ModifierItem: Item
    {
        public int Modifier { get; private set; }
        public int Count { get; set; }

        public string Name => Modifier.ToString();

        public ModifierItem(int modifier, int count = 1)
        {
            Modifier = modifier;
            Count = count;
        }

        public bool Equals(Item other)
        {
            if (other is ModifierItem)
            {
                return Modifier == ((ModifierItem)other).Modifier;
            }
            return false;
        }
    }
}
