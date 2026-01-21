using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interactions
{
    internal interface IInteractable
    {
        void Interact(GameObject interactor);
    }
}
