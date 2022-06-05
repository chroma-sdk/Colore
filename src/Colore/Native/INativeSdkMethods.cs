// ---------------------------------------------------------------------------------------
// <copyright file="INativeSdkMethods.cs" company="Corale">
//     Copyright © 2015-2022 by Adam Hellberg and Brandon Scott.
//
//     Permission is hereby granted, free of charge, to any person obtaining a copy of
//     this software and associated documentation files (the "Software"), to deal in
//     the Software without restriction, including without limitation the rights to
//     use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
//     of the Software, and to permit persons to whom the Software is furnished to do
//     so, subject to the following conditions:
//
//     The above copyright notice and this permission notice shall be included in all
//     copies or substantial portions of the Software.
//
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//     IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//     FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//     AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
//     WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
//     CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
//     "Razer" is a trademark of Razer USA Ltd.
// </copyright>
// ---------------------------------------------------------------------------------------

namespace Colore.Native
{
    /// <summary>
    /// Interface for a class providing access to native Chroma SDK methods.
    /// </summary>
    public interface INativeSdkMethods
    {
        /// <summary>
        /// Gets a reference to the loaded <see cref="NativeSdkMethods.InitDelegate" />.
        /// </summary>
        NativeSdkMethods.InitDelegate Init { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="NativeSdkMethods.UnInitDelegate" />.
        /// </summary>
        NativeSdkMethods.UnInitDelegate UnInit { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="NativeSdkMethods.CreateEffectDelegate" />.
        /// </summary>
        NativeSdkMethods.CreateEffectDelegate CreateEffect { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="NativeSdkMethods.CreateKeyboardEffectDelegate" />.
        /// </summary>
        NativeSdkMethods.CreateKeyboardEffectDelegate CreateKeyboardEffect { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="NativeSdkMethods.CreateMouseEffectDelegate" />.
        /// </summary>
        NativeSdkMethods.CreateMouseEffectDelegate CreateMouseEffect { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="NativeSdkMethods.CreateHeadsetEffectDelegate" />.
        /// </summary>
        NativeSdkMethods.CreateHeadsetEffectDelegate CreateHeadsetEffect { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="NativeSdkMethods.CreateMousepadEffectDelegate" />.
        /// </summary>
        NativeSdkMethods.CreateMousepadEffectDelegate CreateMousepadEffect { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="NativeSdkMethods.CreateKeypadEffectDelegate" />.
        /// </summary>
        NativeSdkMethods.CreateKeypadEffectDelegate CreateKeypadEffect { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="NativeSdkMethods.CreateChromaLinkEffectDelegate" />.
        /// </summary>
        NativeSdkMethods.CreateChromaLinkEffectDelegate CreateChromaLinkEffect { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="NativeSdkMethods.DeleteEffectDelegate" />.
        /// </summary>
        NativeSdkMethods.DeleteEffectDelegate DeleteEffect { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="NativeSdkMethods.SetEffectDelegate" />.
        /// </summary>
        NativeSdkMethods.SetEffectDelegate SetEffect { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="NativeSdkMethods.RegisterEventNotificationDelegate" />.
        /// </summary>
        NativeSdkMethods.RegisterEventNotificationDelegate RegisterEventNotification { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="NativeSdkMethods.UnregisterEventNotificationDelegate" />.
        /// </summary>
        NativeSdkMethods.UnregisterEventNotificationDelegate UnregisterEventNotification { get; }

        /// <summary>
        /// Gets a reference to the loaded <see cref="NativeSdkMethods.QueryDeviceDelegate" />.
        /// </summary>
        NativeSdkMethods.QueryDeviceDelegate QueryDevice { get; }
    }
}
