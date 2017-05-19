namespace Portal.Domain.Shared
{
    public enum ValidationError
    {
        UnknownError,
        #region Member
        InvalidMemberUserId,
        InvalidMemberEmail,
        InvalidMemberPhoneList,
        InvalidMemberRoleList,
        InvalidMemberAbout,
        InvalidMemberContactLinks,
        #endregion
        #region LangSet
        TextNotEnglish,
        TextNotRussian,
        TextNotUkrainian,
        #endregion
        #region MemberName
        InvalidFirstNameInEnglish,
        InvalidFirstNameInRussian,
        InvalidFirstNameInUkrainian,
        InvalidSecondNameInEnglish,
        InvalidSecondNameInRussian,
        InvalidSecondNameInUkrainian,
        InvalidLastNameInEnglish,
        InvalidLastNameInRussian,
        InvalidLastNameInUkrainian,
        #endregion
        #region Phone
        InvalidPhoneNumber,
        #endregion
        #region AssetType
        InvalidAssetTypeName,
        InvalidAssetTypePropertiesList,
        InvalidAssetTypeProperty
        #endregion
    }
}
