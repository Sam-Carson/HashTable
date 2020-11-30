using System;
using System.Collections.Generic;

namespace HashingWithStringKeys
{
    public class KeyAndValue
    {
        public string Key { get; internal set; }
        public string Value { get; set; }
        
        public KeyAndValue(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }

    public class P260HashTable
    {
        // hash TABLE is size pSize, stores data in aarray that goes from 0 to pSize-1
        //internal KeyAndValue[] hashTable;  // Relying on new string array is initialized with all values = null
        int tableSize;
        int collisions = 0;

        LinkedList<KeyAndValue>[] HashLinkedList;

        // constructor --- user specifies how big the table they want to use
        public P260HashTable(int pSize)
        {
            HashLinkedList = new LinkedList<KeyAndValue>[pSize];
            tableSize = pSize;
        }

        public bool AddItem(string key, string value)
        {
            int hashIndex = Hash(key, tableSize);

            Console.Write($"Key {key} hashes to {hashIndex,2}.  ");
            if (HashLinkedList[hashIndex] == null)  // null value means this slot is empty, so we can write our data (now a string) here.
            {
                HashLinkedList[hashIndex] = new LinkedList<KeyAndValue>();
            }
            else
            {
                Console.WriteLine($"<<< COLLISION! But don't worry, we'll take care of it!"); // else this spot was used and we will lose this data!
                collisions++;
            }
            var newValue = new KeyAndValue(key, value);
            HashLinkedList[hashIndex].AddLast(newValue);
            Console.WriteLine($"Key {key} value {value} saved.");
            return true;
        }

        public KeyAndValue GetItem(string key)  // This is a fast lookup!
        {
            int hashIndex = 0;
            hashIndex = Hash(key, tableSize);
            LinkedListNode<KeyAndValue> gI = HashLinkedList[hashIndex].First;
            if (HashLinkedList[hashIndex] == null) return null;
            while (gI != null && gI.Value.Key != key)
            {
                gI = gI.Next;
            }
            if (gI == null) return null;
            else return gI.Value;
        }

        public bool DeleteItem(string key)
        {
            int hash = Hash(key, tableSize);
            LinkedListNode<KeyAndValue> dI = HashLinkedList[hash].First;
            while (dI != null && dI.Value.Key != key) dI = dI.Next;

            if (dI == null) return false;
            else
            {
                HashLinkedList[hash].Remove(dI);
                if (HashLinkedList[hash].Count == 0) HashLinkedList[hash] = null;
                return true;
            }
        }


        public KeyAndValue UpdateItem(string key, string value)
        {
            int hash = Hash(key, tableSize);
            LinkedListNode<KeyAndValue> uI = HashLinkedList[hash].First;
            while (uI != null && uI.Value.Key != key)
            {
                uI = uI.Next;
            }
            if (uI == null) return null;
            else
            {
                uI.Value.Value = value;
                return uI.Value;
            }
        }



        public void PrintTableState()
        {
            LinkedListNode<KeyAndValue> j;
            for (int i = 0; i < HashLinkedList.Length; i++)
            {
                if (HashLinkedList[i] == null) Console.WriteLine($"[{i,2}]\t= <<empty>>");
                else if (HashLinkedList[i] != null)
                {
                    j = HashLinkedList[i].First;
                    while (j != null)
                    {
                        Console.WriteLine($"[{i,2}] \t= {j.Value.Key}: {j.Value.Value}");
                        j = j.Next;
                    }
                }
            }
            Console.WriteLine("\nTotal number of collisions: " + collisions);
        }

        private int Hash(string key, int numSlots)
        {
            double count = 0;
            double hashIndex = 0;
            double radix = 128;

            foreach (char a in key)
            {
                if (count == 4) count = 0;
                hashIndex += (double)a * (Math.Pow((double)radix, count));
                count++;
            }
            return (int)(hashIndex % numSlots);
        }
    }
}
