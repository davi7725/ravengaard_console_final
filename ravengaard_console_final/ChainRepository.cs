﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ravengaard_console_final
{
    public class ChainRepository
    {
        Dictionary<int, Chain> dictionaryOfChain = new Dictionary<int, Chain>();

        public Dictionary<int, string> idAndNameOfProducts()
        {
            Dictionary<int, string> idAndNameOfChains = new Dictionary<int, string>();

            foreach (Chain chain in dictionaryOfChain.Values)
            {

                idAndNameOfChains.Add(chain.ChainId, chain.Name + " | " + chain.Lenght + " | " + chain.Thickness);
            }

            return idAndNameOfChains;
        }

        public void Clear()
        {
            dictionaryOfChain.Clear();
        }

        public Chain Create(string name, float lenght, float thickness)
        {
            Chain chain = new Chain(name, lenght, thickness);
            chain.ChainId = 1;

            dictionaryOfChain.Add(1, chain);

            return chain;
        }
    }
}
