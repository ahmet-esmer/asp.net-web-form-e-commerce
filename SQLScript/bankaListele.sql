USE [LensOptikDB]
GO
/****** Object:  StoredProcedure [dbo].[banka_KayitListele]    Script Date: 06/29/2011 16:03:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[banka_KayitListele]

@parametre nvarchar(30)
AS
SET NOCOUNT ON;
IF (@parametre = 'admin')
		BEGIN
			SELECT id, bankaAdi,sira,baslikResmi,durum,varsayilan FROM dbo.tbl_bankalar ORDER BY sira ASC
		END
IF (@parametre = 'web')
	BEGIN
     SELECT id, bankaAdi,baslikResmi  FROM dbo.tbl_bankalar WHERE durum= 1 ORDER BY sira ASC
    END


