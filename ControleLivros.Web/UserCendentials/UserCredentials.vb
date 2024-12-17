Public Module UserCredentials
    Public Enum Users
        Admin
        User
        Seg
    End Enum

    Public ReadOnly Passwords As New Dictionary(Of Users, String) From {
        {Users.Admin, "1234"},
        {Users.User, "user2025"},
        {Users.Seg, "Seg2025"}
    }
End Module
