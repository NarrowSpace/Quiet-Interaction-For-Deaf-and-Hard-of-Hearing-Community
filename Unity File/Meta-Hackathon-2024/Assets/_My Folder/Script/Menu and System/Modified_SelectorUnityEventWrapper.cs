/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * you may not use the Oculus SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 *
 * You may obtain a copy of the License at
 *
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Oculus SDK
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Oculus.Interaction
{
    public class Modified_SelectorUnityEventWrapper : MonoBehaviour
    {
        [SerializeField, Interface(typeof(ISelector))]
        private UnityEngine.Object _leftHandSelector;
        private ISelector LeftHandSelector;

        [SerializeField, Interface(typeof(ISelector))]
        private UnityEngine.Object _rightHandSelector;
        private ISelector RightHandSelector;

        [SerializeField]
        private UnityEvent _whenSelected;

        [SerializeField]
        private UnityEvent _whenUnselected;

        public UnityEvent WhenSelected => _whenSelected;
        public UnityEvent WhenUnselected => _whenUnselected;

        private bool isLeftHandSelected = false;
        private bool isRightHandSelected = false;

        protected bool _started = false;

        protected virtual void Awake()
        {
            LeftHandSelector = _leftHandSelector as ISelector;
            RightHandSelector = _rightHandSelector as ISelector;
        }

        protected virtual void Start()
        {
            this.BeginStart(ref _started);
            this.AssertField(LeftHandSelector, nameof(LeftHandSelector));
            this.EndStart(ref _started);
            this.AssertField(RightHandSelector, nameof(RightHandSelector));
        }

        protected virtual void OnEnable()
        {
            if (_started)
            {
                LeftHandSelector.WhenSelected += () => HandleSelected(ref isLeftHandSelected);
                LeftHandSelector.WhenUnselected += () => HandleUnselected(ref isLeftHandSelected);
                RightHandSelector.WhenSelected += () => HandleSelected(ref isRightHandSelected);
                RightHandSelector.WhenUnselected += () => HandleUnselected(ref isRightHandSelected);
            }
        }

        protected virtual void OnDisable()
        {
            if (_started)
            {

                LeftHandSelector.WhenSelected -= () => HandleSelected(ref isLeftHandSelected);
                LeftHandSelector.WhenUnselected -= () => HandleUnselected(ref isLeftHandSelected);

                RightHandSelector.WhenSelected -= () => HandleSelected(ref isRightHandSelected);
                RightHandSelector.WhenUnselected -= () => HandleUnselected(ref isRightHandSelected);
            }
        }

        private void HandleSelected(ref bool handSelected)
        {
            handSelected = true;

            if (isLeftHandSelected && isRightHandSelected)
            {
                _whenSelected.Invoke();
            }

        }

        private void HandleUnselected(ref bool handSelected)
        {
            
            handSelected = false;

            if (!isLeftHandSelected && !isRightHandSelected)
            {
                _whenUnselected.Invoke();
            }
            
        }

        #region Inject

        public void InjectAllSelectorUnityEventWrapper(ISelector selector)
        {
            InjectSelector(selector);
        }

        public void InjectSelector(ISelector selector)
        {
            _leftHandSelector = selector as UnityEngine.Object;
            LeftHandSelector = selector;

            _rightHandSelector = selector as UnityEngine.Object;
            RightHandSelector = selector;
        }

        #endregion
    }
}
