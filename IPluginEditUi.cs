﻿using Avalonia.Controls;

namespace PluginBase
{
    /// <summary>
    /// Inherit this if you want to show own MAUI view instead of dynamically created ui from track & item payload
    /// Bottom controls stays the same, this ui will just expand the where track & item payloads are
    /// Also general settings will stay visible from app ui
    /// </summary>
    public interface IPluginEditUi
    {
        /// <summary>
        /// Return ContentView if you have custom ui for editing item. Return null to use default
        /// </summary>
        UserControl GetItemPayloadEditingUi(object payload);

        /// <summary>
        /// Return ContentView if you have custom ui for editing track payload. Return null to use default
        /// </summary>
        UserControl GetTrackPayloadEditingUi(object payload);

        /// <summary>
        /// Return ContentView if you have custom ui for editing track override payload (of the item). Return null to use dafault
        /// </summary>
        UserControl GetTrackOverridePayloadEditingUi(object payload);

        /// <summary>
        /// Clean up and dispose items
        /// </summary>
        void ViewClosed();

        /// <summary>
        /// This is optional funtion, usefull when ExpandTrackPayloadUi = true. Usually it's enough to trust the UserControl dataContext, but in cases of
        /// more special ui, you get more control with this
        /// </summary>
        void SetPayloads(object trackpayload, object trackOverridePayload, object itemPayload);
    }
}