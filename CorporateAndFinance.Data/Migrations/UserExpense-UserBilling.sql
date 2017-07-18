﻿ALTER TABLE [dbo].[UserExpenses] DROP CONSTRAINT [PK_dbo.UserExpenses]
CREATE TABLE [dbo].[UserAllocationBilling] (
    [UserAllocationBillingID] [bigint] NOT NULL IDENTITY,
    [UserExpenseID] [uniqueidentifier] NOT NULL,
    [UserID] [nvarchar](max),
    [DepartmentID] [bigint] NOT NULL,
    [BillingDate] [datetime] NOT NULL,
    [Percentage] [decimal](18, 2) NOT NULL,
    [AmountPKR] [decimal](18, 6) NOT NULL,
    [AmountUSD] [decimal](18, 6) NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreatedBy] [uniqueidentifier] NOT NULL,
    [CreatedOn] [datetime] NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_dbo.UserAllocationBilling] PRIMARY KEY ([UserAllocationBillingID])
)
CREATE INDEX [IX_UserExpenseID] ON [dbo].[UserAllocationBilling]([UserExpenseID])
CREATE INDEX [IX_DepartmentID] ON [dbo].[UserAllocationBilling]([DepartmentID])
ALTER TABLE [dbo].[UserExpenses] ADD [SerialNumber] [nvarchar](max)
ALTER TABLE [dbo].[UserExpenses] ALTER COLUMN [UserExpenseID] [uniqueidentifier] NOT NULL
ALTER TABLE [dbo].[UserExpenses] ADD CONSTRAINT [PK_dbo.UserExpenses] PRIMARY KEY ([UserExpenseID])
ALTER TABLE [dbo].[UserAllocationBilling] ADD CONSTRAINT [FK_dbo.UserAllocationBilling_dbo.Department_DepartmentID] FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[Department] ([DepartmentID])
ALTER TABLE [dbo].[UserAllocationBilling] ADD CONSTRAINT [FK_dbo.UserAllocationBilling_dbo.UserExpenses_UserExpenseID] FOREIGN KEY ([UserExpenseID]) REFERENCES [dbo].[UserExpenses] ([UserExpenseID])
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201707171227544_UserExpense-UserBilling', N'CorporateAndFinance.Data.Migrations.Configuration',  0x1F8B0800000000000400ED7DDB6E1C39B2E0FB02FB0F829E760FE658B6FAF460A6619F0359B2DD9A91AC824A6E2CE645485752521E6765D66466C9160EF6CBF6613F697F61C9BC54F212BC332F250B8DB6A424190C0683C1603022F8FFFECFFF7DFB1F3FD6E9C1232ACA24CFDE1DBE79F5FAF00065AB3C4EB2FB7787DBEAEE5FFF72F81FFFFEDFFFDBDB0FF1FAC7C11F5DBD5F483DDC322BDF1D3E54D5E6B7A3A372F580D651F96A9DAC8ABCCCEFAA57AB7C7D14C5F9D1F1EBD77F3D7AF3E60861108718D6C1C1DBEB6D56256B54FF81FF3CCDB315DA54DB28BDCC639496ED775CB2ACA11E7C8ED6A8DC442BF4EEF0342F36791155E8248B3F2659845BBE3A8BAAE8F0E0244D228CCF12A57787075196E55554616C7FFB52A26555E4D9FD72833F44E9CDD306E17A77515AA27614BFF5D54D07F4FA980CE8A86FD8815A6DCB2A5F5B027CF34B4BA123BEB9139D0F7714C434FC80695D3D9151D7747C777812C7052ACBC303BEAFDF4ED382D483A98CBFA157F504BD6A21FCE94056EF4F3B5EC12C45FEC355B769B52DD0BB0C6DAB224AFF74B0D87E4D93D5DFD1D34DFE0D65EFB26D9AD27863CC7119F3017F5A14F90615D5D335BA6347737E767870C4363FE2DBEF5A8B4D9B619F67D59FFFEDF0E0334625FA9AA21D9350245A5678789F5086C8A8E3455455A8C0737C1EA39ACC02125C97186F54E01586FA4E3F6D9318E8530DE714CF831E6F358C76F86F3A287895E0657F787019FDB840D97DF58005C2F15F0E0F3E263F50DC7D69217FC9122C2570A3AAD822EB9E2FF3AF498A14FDFEFA5AD3AD5137175116A749367C471FA31F83F7F18F6443265DD1CF71907ECECB339422CCDB5D4FEFF33C4551663DC9CBBCA8AE8A18151497FE72ECC0E978B3289E7CE8DBF0A8065BBCBEBD18C5A493D35A420CDC47818864BACABA8EF0EE886EF06E6B4DF98BA8ACB0BC4FEE929E190C81BD3DEA771DE55EF43ECABEBD2FF0BEF1E0B11DF54066B123F5E8B86C4A6CEBB1F625D2ABEF7ED2604D7E57F0F8251195DE4CDE74A591874196D3E221CFD0E7EDFA6B2FC7065BB9795645AB6A81F934CF86EEECC33A4AD2A13BF1DC174DBAD829B3DE3A8C6E5B3C5955C923F2DD15F74F3C7B0AE6D9886457613CA618D608CF202BE659713206F6397A4CEE6BEA4B775284E5C3354AEB4AE543B269AC04357BDEB2B53E16F9FA3A4FDBD64CE1ED32DF162B42B65C56E3262AEE51658EE169BEDE44D91381A1C090ADC5614817C2183235200C8DA50105C94328505066211B287C5C4404D77C2C49D176EB694608A2F65928AC9ABD7CB52287BD71B4ADB633D2C98BC40D287143C8B14E4AC9E55827E92C252D885C5B26C50F2A1750042B3962491A2FF232A9F154A1CCD3856A24A02FAD2BEC1ADA061EDBDC0D1614253EE4D88E8C6DA71D1C5DDD747C4C9B10FBA4FF1E39A7FDD1636FF4DCA4344AF14036EAE5435E5463E8E32D91D85DCF6D036D4169F6B43026983354AE8A64D3DC51C929F4E6F865030D782070DAAB2402507F32304095FC481322978C1066AA4BD1A66AE990A7ABDA0F212BB1D08CB26A91462BB4C69AB76E1050036818623DC54080CAB64339439BA8A8E423E8CB6F77BB538FB7582AE83740155BEDE64B898AD3A8883FFCD8A0ACD4F08B585924335F474A62A1A22D796B0035B0C404EFBEA604E9AE821AE35DAD10DA48BF50FCF5921ED69C34941E2B0F5D8505B25FA7F90B741FA527DB0A6B2EB8CF406AD37CB48033EA86D679E36EE628BD46EBA8F8A6BA36088333B956DE2ABB3153212D47D90E4FE5DA11667C5D47445EF1DC6BD4F2782C148FED513C59130BD18EE5D02A5947E9E1C1A2C0BFB50E73780D2D571199973F6B2FC3921461C1F0107E4D86B07AD82B661A0B08A4C339EE5F827EE4B58309D066B2870978B9ED622098B1F6B13F5016E7458F84AF4538849B5B98BD35D41134987B1559A34176C40F590CC3D18855524F6D6838FED5CC21CD7A423BF6BAA6F1D68A6737BE19B48F66BD0C3B8CB9DB3C789901EE567C25ED51DCA881B08799B5F2DACEC4E13AEF653CA8596C64D01660BB8BD96F23E1B630484D74165033DB747E02FBA9528A500B9B67314EEB95D703F45E4565FB1B404393E4400291B747D989511B731A38AA930D3E33ACEAAFA48E7A96B49585A9D2B7087452097240998538F713E4C124610F48B827738829F89814A5EE6E6FA06B4522F846B1CCE11D30567AD1BCE12D4F0DD471BD88E7142E34BB23DE1E6DB767F96AABBA3F03F60FAA09B8F3023581EB126575A76B1EDD0D1557E99696FAFD40E4B5845D4951D5567D00F6CBA1D420C554C83762CF6DB59BD920DB6B076C66DB6C8796DF764B4319EF122BD866EF6FF5EB0830CD2EDFF5BE7C2A2BB41E65BF0F146EF9336E5EC3EE598AF3A26A8B731295DC56E221273948B310921C4E2E12120031A6618974ED2DD94218FDAFD12A419B915CEDCD9C04C288C1F7499A86F1E9B70D5EB0BC2CB696CB4BDCB2AC0595BF70A67C9D2721D658EE178BE88948D5335469CEA9417A6B85CAEF288A875E52A76982443DCB553F7B51098C5402EF931F7F66323824DA1E5D95882BBD2AA595A40756B903A697F212406B9995BAE2A3A7ECDBCD174679920D85741C4A9971E8FA1291AE6F92CACF4C39662CC4CF23D5ADAC7A8E225226D9CD5CCF8D45247781E593C28D85340B81792EAA225A4179AE52B506B2EEFC0CD7471FD69B347F4223A5BEF1BFE1313C8726F759A4398786098F0B265EAFD13FB74913792B28037E977361065AF78215D7BBA458FB8F761195E5F7BC887F8F4A95A37418D49768B5259112CB2A5A6F06EFCD2C9354F0BE824DCDCDF7FC63B4C22AE6878CB4F2867791AFBEE5DBAA753FFD52AD6C3D50770082A073B25AA1B2FC889919C5A7B429C7CD8E4E36558DA0769C6AF9E9358D9235ACDDF08E2F5D55B9334D5343D06B24D56C4FAB17F97D224900C0F7D05595A3DAD4D0A2DA56B345950033C3B4AD2947B4AEA0C5B3A9E57AFE374395AA2D477757498B725F337C60655FA3898A14F471AA0CB654D0155C625675B1B56C9DBA1FE1D4C09783980A955CB0D59E723A6353FB13A22950054418AAE782F30215EBA42CA54941D83A201B70C520BA7C9D61BC2B02FB30EAD65E60678BCE5854AF1C22DC5567CCCB5D12F29372F31955AFBAD6AF1AB81F0B0C13AB73DF5E0960FF7460DCB83F811E9B9E407F79F3F5EE97BFFCFAE728FEE5CFFF867EF975FCD3A84C6B086CAE539D7EC3A892F56C696C7A017BFA234AB7A1BB7262FE5A5D08CFFC35D8F9337FC75C2E76EB294C323559F1B7C744EDF41AE878D57684A9310756251B43784E2550E7CFA8044B915183F274D7C5D83C3DCE5AB2BB9EF3CE6C46419985CD99C2C7F99E8E0F30DE8FABBAE719161DEAD589B95F6A99A476B0CC45244BE800672CF28EF5B23AD883F76BD0C9DF59B4F5876F4FE9D6039A8D80EB517295712C8469C4DC80498CCC07A7B9AC21AA820AE140171715C677CAE40FE1C4FEA248D651FFBC8FA713C2FB27AFDDAC93D33C983DDF2B68D1A6B75FD2B5A5464CEA574834C3355D8CC3014DAE1A4459BBACD33E12640F99D9FEE1B7774CB36FE83C245E077931CC2041B0F64936BBED298C6BDD38A23DCCF962F6B255A5875B275DE52594222FEB20574272D12FD6505E0801DB83B3621E2696696E714C9E314C42FC92D3EA5AA22289D2917C52F64E996F691C44CFBECCB3EA21C52B3C4A296D37789410DBCDF160FD7CB87A7F7EDBBA081683F5B2F8387C1F8DE7E1ED499AE6DF9BE4C80375F43ECFFA44B0E18781A2F4F61A25EBAFDBA26C73640ED4551D3B864568F73EF540DD5CA0E811DD7EC85651F930E878CEF1AC6744E3B95D2C2F07EBE5773C41D5C3ED79566E8B41F9EC12C5096E7B7BB5381B8E9793A67A2B676E177FBF1EADAF2FCBE1C615F89D094F6BC4EC755F03BB829366091DD4150AA829BA8CC35358E72815C6901395B332DCBB3079EAC33DA059A8C4DECE36CFC312DDCF8AB5F2EA760163E95507F1B9CCF3CE89C781F7C6C23C35D9419B05B70378793E3D4983196B4D58BD7CA939B485386385CEC060F24AD3F0AFF3496E65F5CFF9F9AE3F2A4744982548019CDB2AA450F35C881CA43D5C8BD408822CCBA9738DB4394026E9FB1AAD50122ECDCEBC52D28C99D6E71473DF7D5EE89E361CC813EF2657AD2F33CCB1DC410562DE9532B95CA6560FFB96D04B7A3778C1395139CC819D1E892DA8D0CA86CF83B9862A07F8C8AEA3D611FBDCEF90E633D12862C7B7E74C125E8E773F3E90149D4476877508ED97F514F2CE7841F9DF99CEE9BED4E3AE74823C8F5A9790105E1AFBE409E2B4028D597D81AAEAE9B44E17E1CCEC3B18B360F71D362E0CCF341EEDF4F910E129BE3B59ADC8814397066BCC9747C3F435E32371FBF7BC4E86C14E1921E25CE6705031966654B69F93CDA6C81FC9B438CB3500DA2C241C80978BAC9380194BEAA93233599B5242392D05DBB4750F17078AF9CED76D628DA13B7A091070BEC807D699E4425F5D53B83ED454B74ED5D383331E07D3463D10EA9BD148E8FA5E97A20C8E213683B96D029EC27F4CA17F5EBEDFC6F78113FB054AE9FFCF2D2AF5770D61E469B81DAB4965BE44C563523F3E3E34E2C96352AA0F0D613AFA8CBEE32FFF8956D5E05DDDA00CAFD029C30ED50DFF967FD51D0A4D31B464AE4F4514A30BF4884224C2B45E90C44B16C537F974E33F2FF159138F3EADFBF313589FF3ABBBDE29C7C7987A12C73594287D8F05F15D3282FE47C05FDDB5EEE4B43FF17082665B8C956A96881A6E62DC77A5A81C01E76B84A53056898BFB28CBEF8B683D428F1BF260ED02AB3779A6B1D10ED0634011A095B5F8CB5D92A28FF87FACDF0C9F47F7AA7A2091B49A375642317BE710EECBE9EDE68C62CC8B78FFEC37B5D0D630125BB2AA0F9FB5E6385C8841545E449B2ADF2C4E7D69D3C069A67478D98C11276FE6943D6D5CF1C6902EF36DE9CD1B180E933DDB154E0D6444327E599EF5EAB8C7E0F1B68E4F45A86A9222FB0264A18D450E2A51E632BFABBE4705EA127E8F8542B8AC462359E39E4FDC8AB17165797112C0D24E4199857185C2C7C5B8C2351FCBB812CEA8A08DD7087422BE38D15CE085B2B6985C76061AD3A0D7F564625A85080F86E872F528BC62990478B358822066AEF1FE20A0B19665B88403232DCB7072A4A57610032D3E089248E3E8DEF4A8E16058A9EFF5870CC76D7A18360837AC8BF273D0663C2EEF40E9A188C755D505E3FF940D3CD2D5588C8669A51B0E1D9B6B361EBA8577046F0F3ED8AE37C3EDCE7F9F7BC933E978F91978139A2E8BC2E4592EDB1B5514873D978483F6A9C8B71BF6C2D70DD08BB70F04E5E7F4F661C5B091A660AE220CE9DEC3F524F1EC91D7D2A1AEF3E7D1E9352648434918B8621D9A61128DDC44E5B7EE4AC74B4DE901CD464DE951725553580863AA29A467DF6D83B05700CFEA9B3C00906BB48E8A6F6398E267E6176B7B0C7D88B27BF4DCCFB21D874B452529BC6557A090F819AA03662B052B7AE72B6DF0F79699B392963E72724C09A973B11826CF4258EB7CD8A8DAE35F83BC7795E4454D7E1FAFAF6086BDB36D98A4AC3F8981501097A1652BA4902A85B0936CED6440F8D7CEF6E3A533BF1479E305DC0FE35BE7F048043DBDE2131162A9C0C740151B0E3E29CB7C95D45875B749248306F9E73DC901FB804A8E0F3E64F141D3B550B347AE212C19609313E412F35E425C5D300A785205CAA980EEB422166853CE83FE1701749B6DA54AA2943C408B593FC92A9193936C956CA2543734AE21B808BA043C477C2F47BB6EF89233B4411999491D15FCFADF75C32D511D8DDE1E517CA2669FEE0D002A078B9C83A0CA1013ED5E2630E7231034C04A4CEA9A417849354893E994BEFC67C5512A7A7863313E5F911F69428E1BC6DC45351986C7E80EE49CD6D71A87DF80614FC6750085F683F7BA87CB6FFB5FCFF2D5B6B92890B387AA19CC835D353B3654F603B2225F6D305E342181190F74CDBD59D2845A21501A8133EB2726A322EE6EFE290692718BBC09C4915C6D3D9B1875A46449BE8FD7AF5E89DCEFC48D7A8C46E544FD4CEC1917DE7223928B46690B150FDAC844790700EF5933B917F749C76C32DB5DDB40AC27258F3F2EE3EECB7FE0BEF2A2FF60B42B0B8D06D993C55E0016E42B0DBF1F4B073FD56E2CA5D39E48411E7F4A9E2F488CA55A59346A0DF1A78E73545C6AD6A972B7DED51B8863ADE862C2293C404F06B6226138FC46E0672A36AD76D7384DA3642D6760B83AC4B15C4D237D4FDB0FC0A49D69B4DEA7482D0D8B7AD0E622BF4F3273DA34D587A74DDB8F863675ADE168435033274D5D7B78CA34DD6808D3DCE20C45974E7D32A7CDAEC5F0F4E9BB52E8CBE16853436C6D4535F64ABD9EAA283D2FC0D63CED7995060D8CDC9AC81634005EE295A1AA7A96B74796F68A342783E23D5F176BA9D3A62FC7613CD3A59CC6FB60B2A4C341283E50F1BEEA913B7671391A8354EF339B706D402B901C1593C9553D376F7D0C97933D042A23B31AFD24A1092B80EF13066536E86543CF5DD39BEB009C4CE65A74AE70663680EEA6169F49F98C5A2486520D6C21E335B71D53DECD54E24D89CDE8124E39057B24E428E41B4F1D4386E01C77066039D6EF672AF126436824D926A3F85E08B605F7C6A96AE217B2074F59CE5A50CFF0DA7116DFC1D48C25C16724BE92907BF66CC51FE9CDAF48B42D87B73F0C7169E2E46D6461C257350AEE7134373BBDC9E0C73BBC9BD0691F8EF1B43DCAE8625DD6C082FF342B55DA83F7C5BA2B5DEADF1353A274B503AF4816B68C16A65649AF25080E73FCB50752648F169DFC696C0D2FE89FBD36F33F36603B8327B9C1AEFA4CCC4372A2961016DC10C0A9DE946641B09A8E4F9957552DF9077C2E75306E851F74057B631E7A1F9367218ACC826D21E2ED0BE7F263D1C418E9C2439C628CA60F0C918ECD641A430519B9C6834CCA3F9AC79E64F36EFAF253CF04E05377E6C614C3B7A3C6340F9BA134AA8DD86C5AF6C450AC7BBFCB8655C0BC3F833227943E08EE7144F60490326106E5DB5DDE0C0ACC4D10A446B2372BB36DAA6CC366A937D9632F904BD9CE206D94BE73EC4B3613A446BF6B33999E3D91A4F0706897003B96A1EFBFC76354BA57895548621D1B945501B44C2F34A42FDD076056608E82A0353ABBDA8B532B39EACE97F3929CF31299CF45561A299CF22683B29FBB8619F64E598DD2B8FAA57E2AF653B5D4BB3248D3860EC079B3706590E06332BF015C1924E436DD6227652B30F3976AEAD569C058066BF221DA8517AB53388AF0BB4CB5830517AB066C3AC57022456B4653D1C61F9511380EC8CB25E30555922E31AE088A29D2DCB82B527C0D10B8F4A18682E987272D43C5EED6A0CDF3D926F9AC6B913B7731C91D46A0CD7357B679D2F881910E96A8EAFC94E2B84025716A38F8B0CB51D6C9E6A60C200E0B83C9572682A1F38519409281D03666AE344418CC958211A8041CCECE8DC00C1D3A579214189D96480B16708A820003BE471AD0A28F9A0857F4FA32C6578DA605A03EF98F0A609F5C47035870EF11A10AAE34862055B08C8080CB925799347084485C082810AE6B01B68DC055836D235D2DC036C1AB6AA88DB435990FC59A66DC830C60F5874E1934FA28AF01A80666018832C4C8D0B261E0DEF556068D76723617CEBD2B8F5248F75E32E6A019EF0B2574C6A541DB412C11DEB19E928A09319D8C05AAB04E1095F0F6BA2BD5C2016E522080E0CD9639680D482D28EA65510814F31AAB01170B664E193303E6672BF07AB8460099D4CA2040FA3863085005CA5822EBA4312889293D9755F3D82CB707543D4AE753A4C26594747932DCDD6076CAA570A25181E9547D0E4C8F3737DC2376BC06B40053B602E4D0A7766586A24CEE4A8D86DA15158451E67315A03D8194F6A70DA34A6B29244D52AA1A1994A6D4975A5066529166CCE002504E993413A49E799A4D6EB44689369911534702251D8D526B8290A9B17A1393D3FEE93C8F002115B5E54395378208289E581454548056523024E5C4100A05E134F116E0E0E4211700D9CCE8258FB1B09809BFA50B1CC8950B571364265B5CF20833BF452B0F23A3E00263F426A159323880960E59E498C1DBE591B3A1824337CAD54DD3C19BDE92646500814DD29A3143D52436E396A29A6C9AE465142CC864129C4A9DD1444F2528C19972645C8A332F2A7169CC2454EA06139C4AEDA9434F2420D399725C6CAE332F12B1F9CC24146A07129C40D4AEA627525FD97C74BB366188D58353ECA7C1940F3AAD994CE790A63E13F50228F9198FBED1194180262146084200B9CD0052E832A031E82B72A05103602C9B0A6228B29E991CBE1C7903C8D025E1105D2E2F616615D9BCB8F93556E115F9BBCC28EE49253AB994864CD23C54D2414199A87C0905E59E1A6071B1C67E036632C8A2248C499D47891B95E9AA93031E85B184843E7A7A01AE00AA11B1EE0061A8C47A000CC44F7C3E1A096594696B8441C812D77063606E7D347491A5AA19802CFA7C2A06DA8FCDF9D83C0D8B973634D91959996445695DB53A111BA565F1B0B04E7AEE95260851D0CFC2B8A54D27E24CB7C94C5B50E2101DB1E00C23F271093946FCC824A41531D5F7DD09244F7E21A79561C20C689CFA941986573C76C0D5F747D49887222BEB2D604C59798A0793F183491E02D2174CEB00C367C7EF4D6531AF80EC56D7F00E539E7EC0FE5677E49B4B4D8C3C40189BA87A66788671F5D458613F1005050D23E9073B2EE8C2BA0DC9290DCCD10E168ACF094D50283607EE23D4314319862C397398872E0BA705A3E0656E7385BC77348712A370E5414FB6EAB85963C2D28D6C874C1B4286242DDD8F443F0AA83FB2385871AB0B9B5AF1A70DF526E4489DDC54D4361E9D465A7AD06D4A19A934C528C3EE54235299621C893486C5138C0793D0461F3B268C46193DC60DA9F56FD4D047192F0640DC8DC99B5C40301340285DC813332045D01370F9085E3CEAE005BECC7C7BD400DA4536EDCADE1E2D570F681DB51FDE1EE12A2BB4A9B6517A99C7282DBB82CB68B321BB54DFB2FD72B0DC442B7220FAD7E5E1C18F759A95EF0E1FAA6AF3DBD15159832E5FAD93559197F95DF56A95AF8FA2383F3A7EFDFAAF476FDE1CAD1B18472B86DE7C1CD6AEA72A2FA27BC495361EE21F93A2ACCEA22AFA1A915DFE345E03D524715C2C197734EFBAE543B5C499EC5C72BB16E4772E76EC248BDB6E5F112C77D15D1CB09EAE1FF150C926548F1A518C206D89DB2E57511A155D4C218B3989273CCDD3ED3A133EF3CC2987D58619AE100F8D29308747A68E07D57D3387D20EE50D38401B4097F9D784B851D360BA6FE6502EA22CC6FA1907A7FF6A0EE963F48305527F306FFF8F64D38449D030761FCDE19C97672845158A5948D4677358CBBCA8AE8A98ECE6342CEAB30DFB6CB3AA78E2F9A7FD688153859728874FF3C90217BC403944EA2F16100A84FB8CAF320E4CFFD98607CB0A4BF0E42EE1278D2D1121BE3DE224102FEF8E0481C7ED43BC043592AF9A580307114B45BEDA4B59556319CDFB36BC48634BCC67B1CB34C9C3B28452F74C7EE72051DF6DA1896285FE6E0E6DF19067E8F376FD9597074C818D44C0FBFDAA5A602D2AE7D7115B640EF3C3BA8EF6A161B59FC693E4BBCDCC73873B2F4F5655F2887841DE7D7D9156C6D22AA09872145076A2C95F908822C45678BCB05F10F6D35CE13870219D9FC19E1995ADE5A29AC9C6CD8A6A65A26E2D4C09BC4936DFE04AC1C96A45345C68D3E48AAC611246012136052FEB7C92750E7B5FB82F72F7056EBDB8832C44FF6D67F9804F972218EAB3B58881961F57640D535C7E4C8139BC3354AE8A64D35C33D0F0988297E53CCDB6AD8E8D765FD7541E23E715AE82A1E1DEBEA964D5B315A6D9D42FD07D949E6C2BBCF2F1C8459100954FB7EECE04B3D499A555EA13CA5011A5D7681D15DFB8E3235F66672FDB72D0BA6F36C6E4BA63CE6CDB7FB587442E4744CB345B660DF5188477EC024981DFB13D7E276BA29A71FA5AFBCDC23291A46811550F9C7962F77546D2D3C8F7D9497E8AE9DA5C24A8011485DD8A6F2CCA3BB08AF94CF38EEC7C0750F9D8573921657D38AD25E4FDC7C7225F8B72BDFF6A6197CC6211D0EEA38574C2F545D5B3FF6A657D6D79E75A408C2FB3E60908245560BB0E4470F4F7178DD648269BC4F438086421CFA5BD34D68398464C422A80FDD62FC7CE15AFF0C21296952F4BCB5ADD09AEE57829372E3A4D2826EDDBC1E617BED4460B2E4AC042447DB66335E06419D943FA44727D73A3ECBE8D7B7F19C633669E1AD98B98D1A69BF31237BB5CD03E62470E442F2CBAB6723144D79872170E7388EA46230A21B6C41EE2F2A9ACD05A0E972E9FC625EC453004120C06A1D20E528183EA2012B410549A37D51052C2B9623BAD9E349601B55CBFDE66DF6BB44AD006D492B8A2E98CDB248A49343BF45FC736849E97CB2845E5354A23407CB065E650A9305F71B042A1B54919B4255B39E0454F64997529AD19173CB6C842E16C96D0EF28E2C8C81458C8E1348176EFDDD769CFCD2FFB4DD0FD26F046E3B1C3D86F2D61C47F201311EE177028D87DB583049EB7A9EF76D02E1169759354FC9152287C395BEEF9920E152FC53DBB63BFAAB510A4ECC0F381DD75CF2C0D4B1FD69B347F42A0E73D5F36B69108AB93C97D16817A665F30CD72E61E1A65752FE51BA4AAB9F037CFD50DB00A7397146B7EA07C998D6E5896DFF322FEBD7EDF86550DE9128B933E5A6D8963CDB28AD61BEEB4CF164D17424235935014AE617126F89E7FC45A7F5E7CC8A2AF290F5D2CB59010F9EA5BBEADDA4BE02FD58A131562B1036C0067BECCCAF31895E547CCA2283E058E7162B19D2225CACBFEEB6C364A20E379905D537C57CE7EDF348031CCCE59ABBC31A0065B9E1D31CA801EDC7FB684F547946E2160EDF759F2943485BD274F358F0AFAF19404C6B05C51778ABF3D2682D5992BB2D837DA367F475C1C3053304BFE9065EFF7648FFA4D323FEE804148D5335C9B678EEEDB5082674A7349D8F0901EA44B8888B2B5D27002BBFC7145E37BD83C7747C4A089187E7633079D352CD86294674D335C8F2A00AAE5D3B78316125B3AF6C294E3E68A57BD43F0A7F9F69B554C4451893777D4E7F13D7B430A9E4591AC233ED70AF5D95A58BCE7D3A5F49F6D4C4E8D38E081D1DF5FC49891180B2EC2BCC4978BE80A2F1A44EB81ADA53564E8693F0AF140CB974D212442EA4C3FF5526432B7065327FC7C6E9CFC6D14BE364E7E364B5424510A197BD992FD574A5AF2006A005D60B35166D543FA74BB2465C266C996B9423D5681B50A98FC70F5FEFCB6BD88E2AFA7D8220BFBCC470944A6C0F6B6EB96E47EFD5E074C03F75E74A98567549EF1D1B5ED270BEC509DD53C597FDD1665138CC8E207945B7A3561495301D7747C99859C46D123BAFD90ADA2F241C4582CB5D8E3F00C64643BBB5D2C2FB98D8E2D3287F93BA660F5707B9E95DB429C7FB1D466EEE2047FBFBD5A9CF1934615D879DA918B997625DE2EFE7E2D3ADD0915DCE17F599EA9E1D71526D150021E3D7E7A0D65413D56164C49E9813AEA292A00035E5479AB103DDE3C2CB664362C60F8DC915F32B20EB25F52323914CD51866EAC48524657B13E2E054D80E6EF3A6EE74C3D3DEF691E84F2633F0AB81F072A01193008D55EC12B5CAD6959914246E44AA1707A8FFAD6DD5D84C91458C560A0441A8341178D1DE9103A92E314CFE07D5E40F9CB98128B39CE151C2914DA632ACD630F5670E24D28471250FC33BB25CB574EE05706026AFDF49878807CD98CB6CB385CAEB9D82DBD1CD44CBE1D850883F537DE03FB8BA5201BE0923D381BEFC58133AC39DCDD146E6D069798C09DCCDF73E0E870C61897B53111F72D50553D9D4665B0C72976001D5850D1566E5C689B88B605AAC042143D447892EEBAECD040201754613A0D75DFCE22A1229AC3698C81BCEA66A1774E2444C0876A83481300B2835C318222D7D985C68A0829BACAB4D157E16F6E036E9041D2ECE2C372FB9A2967D601DF3855427A71DC9AB95C092F4FFCE48893FC08B6B6CFCBF7DBF81EDA00FBEF4ED247D42184423BB8A894D80999A229A55A936D63898AC784E4ADE7E29FD8320B3C93C7A41495BCDD578BA311FA8EBFFC275A715A13FDDD42AF43D9B6E0D5B9F6DB342EC17FCBBF026A76FFD51CD2A7228AD1057A445CD031FDDD867B89AB078A6F721843A8DC660563251A63449E3BE7D7305D62C127F9D5DDEE3A90E514A6C442578FE3BA4D94BE4719BA4BF89D162AB73BB05FDDB5FE49A2530C506CB1FAB6051466BFFB6A334F649D8164E58A6C382B12DEF0EBBED940C1B204AB00C57D94E5F745B4E6010AC556BC4F32B12FEA170545DB0C50EC085BBAB4A02A5632057FB94B52043F0200959B43BFAA1E48AC8898578A29B0DACD5B37226137DF7DB7B8ED6B360514630EC0829DBFF2134A2DE441F3183C56616B258313067CA185BF59545E449B2ADF2C4E395733BAC046CF256D764FADB38A2E5364852349EE55F2C3A6BF5B41BBCCB725EF59B7FB6A050948B3D17FB5F04C250D40A2B12556B87D599EF17A5AFFD50AD27956A122435593374100C917DB7850D22D410248AA58AC9C3E45CF32BFABBE4705EAB282801D9AD49FC65A16E8E4FEE2AD18E684BCBC38096C71A3203A9C9095ADA53CD537E285055734E539318C9D180F48B4A7EF3E4E778FB047575684E8ADB681074954245C16D0515780EDE8AF6B0047C56B427388F9C04A763C1D3A9269AE71472D8144AB055360E5D64CE22BA27B5EC3A6BEDBDE8B09F10BD4675B5842AC02F57952DFAC975D5F28F7928083883E6F99E72AECD452EE7988A221AE04C28AA3A0577DB3CB1ED1DE3EA0583EB5922A76BC374CB6914F45BEDD40972A4CC1CB652C69F472191B74D3B989CA6F9D0122D8A6D30375DC74540054CBB36F072D4FB6D46EE193B6329876D0C8F399D036467FB7893E8060F55F6D446898571A66E753F21065F748903DBBAF2FC2C24A580416131E02C25E348459C0C0F5D6F44EA3DEBED28B22A9DF91E72F97BAAFD31C30CFB6405A91DDC79783EA64A9580748C3EA9982D52EFDAA5F183DF9D72CB861E8693A29CB7C9534F74AFC5C9DB0CF48DCB6E9BA1DDEB0D835B5CEB88D5922965F9D51B06F97F9169F6B0106309A3F1E28349F84963B649CF1BC890A721BEE88A7355EA779D6F8E41C9C979FB769FAEEF02E4AF97B66FDE8DF1E819CE2CE4C6D9E6E1766EA9A5AA7DA3698A406F6FC99A9C5D38E996CB23DEF255335BB8A0B4FB52D6DF3731BCC540D79FE0CD5A069C74F7639C87F529EEC1F7B73E14BAAB5E9DB6D0693BD833A7FBEEC51B59775B6EFD4ED057F51C9E26BE218B195D8C822A73D304B3C3C4FAD2614F30868D9B1B77B8A7E7FCE194B1CFD81B2382FFA2740DDC41200856728BE8A8398127AF1145743C928114FBBE5C0B7E7990F2ADF1F0EA41724F3CAB645822AA0B1E563D9C034CA605B72993A15F4ADABD4B0C0D76D67747F1CDC9FD7B4C4F1E63ADAAC5353CC8CDFA066010E0522584F51C6000C20D200049FFB594043423D0776B635DC5B1525192AF82A3BE35DFB65F777D97D20AC15DDA3CB3C4669D9B75BAE1ED03AAAA9526EA2156A5202D50FA9122EFD1A95A8A97278B0685FDC7A77B87C2A2BB46E7D76FF9936918A7D85CB284BEE5059DDE4DF50F6EEF0F8F59BE3C3839334894A92513CBD3B3CF8B14EB3F2B7D5B6ACF27594657993D5F8DDE143556D7E3B3A2AEB1ECB57EB6455E4657E57BD5AE5EBA328CE8F30AC5F8EDEBC3942F1FA886FDE823582F2FAAF1D94B28C99AB5BCA32DCEDC1715C10BF7E76CEFE8E045EE878E41ADDB14D21A1C3B77FCBEFFC7D5382CBBBC3AFC97D42A85CCB884F08330131E22FA28AC407F43C767840D89004EEEC58F148D91193FEABE96A9B25FFDCA2A4867897101DDE126697568AC5DC124847F7164AF61815AB8708237319FDB840D97DF5F0EEF0CDF15FACE1768FE7366021A8BFBEA681568518AAC4C3BC88B238C50B332CD48FD18FB000FF916CEA9C610AA0C7D640A9EBB26EBEED279B72F66E80B8704CFD9629C9B5AFE0187BA2918B7C1418E669BD52C382EC6FF51AB831FEB34AC8658F2519D90B3D2B60F4259152A8922C93EF490EF70717B9DAB77611AD6CEB61A56B974D93ED85DFBA7E3BCF62F4E3DDE17FD5AD7E3B38FF5FB74DC33F1DD48BE2B783D707FFDBBEEF7A8CCD7D1FCB6AFF631DFDF89FB60CD680A3E54720CE659E940EBB2688BEB4AA1675306F60D86D9063509894C00F04D161133593F99D9394BBC8DF3F81E52AAA5C85D4F0E209120E9E1CF7F3B1069538D98543947997F58CC2351F965F282B8AECA860B0B7EDA0F86D6F536EAD0A15C2F698D364731C64FFEB3245D61E762FAB3CC42AF758E11EAB3BE0D95C2BF25DCED4D4CB8B8115B87AF8ECE2705C682D2C683138E9C38C03AC6AD4C4FCF5B2BA8C5617F99126F5D35EEEEBAC07E2B1E258203FCFAE7A81EEA3F4645B3DD4DED4B6C262E2757346596A9CD9BD99D87417501216C52E2CC4584C1A1A716B64DF84C6B683DB05E9E8F7200BA8C703617B1C16DB2E4574CB566895ACA394DC76E0DFCAFADAE20D5E03E49A0917FFD9FAC8BF4B02E6B3CA2CE46C77B5BF2069CEC4A7A04D25AD00C64DD682608695B6908F83F5194204E22575835C98CC691B09A1C104B95820C18A41B6855DACB71C8E918CC2ED681514BC83F9D561EE3B4EBCA691B41056362C36681FCDD21A76187BA51BABFDDBCC04B6DEB14B2FADF58233B4A836DFC82D1789B78C0C24F7438AC9BD3F2CC2F35F3BCCF0BA91C11CB56DD9E91958A7F253A5FC34A841D6096B7871514A6A57A2416C4E84D386389F62F945F921642248BFABC23086B1219C5866A3B3CD421A392CFFB37CB5F53F517550FCC4010D6568EBD5DC76D520A7A98E8083C8AE0E78E3493984140BE250F513AE66A573BFD952D6FAB5EBD7310062784DBB0B56B33686F48DBD966D9063FA353E21A2CD30D7A9A6966B87E54A12815A5ED39ADD2E3B1B2F0D05C412B72CAF511A794A09E15DBF410CE4A12DCEED2BE6DDBB0B6181B78B9FE4F80F7DAD5A7BEC8733918FAA028CE6933C8B8D4B6A83BB0D6AEED59B7D3D0EC870FCB8DD66EAB38BEE9FA10A233D84142470AD774663C89788406E338CCDCEDBE8398B0428198083C94C043382F58CEBD445464021753AD90091C8F350B77FA6AEF689B3619CEF59135538153CB9CF2248057772E20A2215B8C4D932B383735481D3C86A48589FBA4B8AB5DFF01651597ECF8BF877F23A7A08D4BAF7849655B4DE0481A80821F18517848437DFF38FF87093171F32D2CA0BD645BEFA966FABF63EFA4BB5F2BD92DE01F446AD79A4EA23663C149FD2074F17E313D90D20A16730A1C69B8F262BE018DBCF70FAA8B0C3392D859A2C9032EA0EED8F28DD8601B7CF8A8F2689A0F9F1C885FF6006F15754C838166D1E803092B8058607F4C231CA7420660C23CB941190B7BA2E02F3D6402C3B438680B60B2E838803861C8C11F8559AE9CDC2F4E31E3AC335DF2F03D08B2BE740579A33B5E0EC87A713FB7490EB9A563D3E64B6AC590863AE6CEB89E1179F6FEC8C7CE00608D1CDBD6442BDC50B44713306F4AF7179AEA1401EDA61E4DDA248D6519F8BC6C3D8FCFE29D8B6423F3D15E8E66F0EE2D4587EF9C92E3FB935A6CCD2DB8A5FDBA741320AF1B54F1FD5D3C5F2C66BB440DA19E962FBB5DCC8FEE7E947E5E94325F84F798BD0252A92280D686A7ED12F003CDA790BA2165CE659F5903EDD2E491ED0DDC617DC158AEDE678B07E3E5CBD3FBF6D2FEE8AC17A597C1CBE8FE682F096BCB0FBBD0EF91FAAA3F779D6875C871F068AD2DB6B94ACBF6E8BB289A81DAAABDA496E83F761E60234783717287A44B71FB255543E0C3A9EF38CBC0F8CF7E7DBC5F272B05E7EC713543DDC9E67E5B61894CF2E519CE0B6B7578BB3E1783969AAB772E6B67E9F7DA4BEEAF7DB07F3240D96F224E08169FFF4AD052AD649590AAFA08F7195F95398607A025B8698B8E4935BE48D5B89675EB90E8C677E391ACCB033AE4C6A677EDA0A90E92D880A1AD40BDF858F28EF7A4F56A22079721307E96761286AD841786BE0C08936C26108D0D768851287509D3904BE0C180A748A39E13E2FC04475DEB7DD37B97225B8616AF9848009688AABD98C593F771823B48882D33EBC1A4D8FD819ACC5BE173BA60F8C1D330642E1C6E3DBF45DC4C1103226E43D7DCF2DB33E797958B93D2CDCA345071B5CFB585FCDCCF4B6C781798DB96481AAEAE99478D13BF0C9AEB10BA7308D07D6B4F13C5657775DEA696D34D88019441D408FA49DEF87921B446F0AED19370735CC78C5539141279B4D913F46A993D3AE08C6450A48C00C2B0F94C15106276FA6BD9F296726979B41B629308DB05B4846BE26830A04ECC567693811E2293A3C45C6F0A2E2BC7CBF8DEF03466306CA2883B9AD9498A91C5F0E900B22DBEDB0CEE4B144C563B2426196F059F2582B0F41807D46DFF197FF44AB2A08B81B946D0B419F1CCEEFD404D0DFF2AFA0962B606534A19F8A284617E811E9A3800DB997F80BA0F8260F8BE77989555B8C659A64F73EABF5737E75B7BBE5F2B0179CC4CD3BBC51FA1E0BA4BB24D07E46405CDDB5AE30B42F84DF12DB162143D8C922E348E8263823E0993C279CAE119621788B2DEEA32CBF2FA27520A81B92267E513FE707D924FCA11A2F1243C980BFDC25299265F977027B553D90100828E5942303758E1E3EDCD38A7914E3B9C752BA17ABA14FA5C45F6C552B99B5B2309CDB50545E449B2ADF2C4E7DE8D2C068A62B8C4CC288919C60653F7617BC3094CB7C5B7ACD3986C124AC70815103084C9E2FCB335AB3721AD87946B4595435390DBC8241184821874A059D2EF3BBEA7B54A02EB746C86EC244EF053CB1FEE47E5ECB8B131F6B12D5DCE548C8351FF64818F0A8047B91B99D1D2E4EA0EB8BD0CFC8B9E136E005062161BBFD627489D640B47F07160401B90679808086654BC3D8124307C51D242FB36240FE9E8BA5B49DCF20661CACDF1367F3E8DE54333DB6D74CEBEB98213DB29B1E86F5C30EE72BF3D36ED0AC54F21790FE92F1254E7E9A38F9312EBF0CBDD6EDA4DF34111CB34B06D01AFF511C58110D08EE53916F37415E487EB954FC592E15090BDE44E5B7D68AE8B841F5105C372816C2F01B14E9CFF9898BA6B1D77E405EA30CEC107393070628792AC1D172349AAB83A9735A768F7E6ABD9470B1CF82F759EAC32F72DBECF3BEC11E6E6C08198F142FC09AE5C94CEA17DB7DEE50839CFDCEB661D236FCEC67483A5DE31E27C577BCA69591E9A42CF35552EFD29D8D28CABEDD927FDE93E0FE075472C4C207810342C3A66687CA12A577AF9A0F97DBB44AC83D0AEE0E8F5118100BA0E94500D37D6681FD8B00AC0DD7A99288E4662FAB22C20B559CCC245B259B28A511E72A8173DE05751DF1108F7620F9127C1021EF5D66953846BF1E7780391ED451E0ED1135C7EAA9E7DEAE21B8C867BF4B444ACFDCEE9B390F507D41A020960AC405602655C9B448D3A65AF1023D26EF5EC7E707F2234D48C68FB1B8A2EF110248973E2F0EA146B61F7CD2BD32762B3E98AAE295DD3BCFECECF69F6D384678351606DA170FC433F0E3D5D2A993BF566DC939EA57731DFB1F817BB8B7396FA5CF7F7B70CEEB57AF54CCC33F524A0315CA9E15DB289F679D3FCFDC72F8CB85CDEECD3C686E6D04CDF4BC023FFF27992CF97B7F83F389BAEB71F7A53F705F79D17F186957E2BB65408A85CF4AB408C3DB13D9C2E34D6D480BE2D8AC566ABC66DC4CC1D961216150AA7C1086729A58BE513075A71F6D383446E032FE7D93FA2127395BF1AF3AD2332F9459E93EE2935D346CA074109E52BE5A29994BF0D91D7316D2BC5526E9147ADE66160C543F1535350335EF6EC918A82D7D8E0C043C38B66F0C44063535FFD4C67919FB3485CF917BC43B897D639E4ED79F8281AC8E76FBCB3056C7BED9300AF56AD82DF01EF568FC01D8A599EFCF8A4B2CECD6D37247EFB6770B221DFE66817AB28686447F7E267709B2C7795C3A1D4950742660E9BB421EF3A89310327BDEB0B63CCB49523D99642D266C6C79BA8E476690F6E7841BCA34EC32D186626BF59D9447A83D650A392201C8173D3B6912B8EFF19985FC39D5F1654AA69948A658F2CBE46265B17BA762CA734C8F85C02874D173629485E4059639320A7FEC36BF761C8E6DE670073901EB38DD524D7B1AEEFDED2C2E1C8379DCCDE57A710AAF3BB7CBC4A98FCCB45DCDC809C68257F6C0636A6C3671F395625C68E7C12AF5EFC9F05EBCD3195E27628E50BD8EC71075B407F0249996376C1CF60DFDFFF99C8B3CD8BE7C489EB1F5CE0F1023A27DA0CE1583E9F888CAD33F052BD1EFB3492033559E2F43495FAA9B314FF1CC346008DA74E1476306A1D972E2A4B30FBCAA30BA21177A6782860A963F2B93AEF6A50D470426622079D67F70D26593BD9F3C241DBC640E956F1C8CC2455A0C4632FA0AA91027B952125344F2A729A0C6B39246F018F7441EC18C24BD57F5BB68DE1F5E72B958963EEE3712376971189D9D2616480AEE79D622683F65CF506A9081B1784A96994EFF71609AF9A93E53DE774FC93613DD775BF2CBE48E112411D92E9F5FF74C826A4EEB8C69FC6C361FEDB6212AE32204AE2B1A4C7B1153BF29E608CEF566CD1DB22C934E3D8FC02174DA2D8D8B1593A10B0A02820280F620B8489E794C326953441631F3349207C487BA47DC06B373868A9D053A461F93A224798BA3AF11703C22AD96A8EA7C6EE2B8A85F94F9B0CB70D66D3F5DC972F580D6D1BBC3F86B8E19A0C991B62B147887054F6723137AA00BA14EE872837E243DC8616BA132766C0138530AF5C15430EB4ADE8DB20B53F074D2275947741D459774356DE780F70BD03D500B4600A8A84141F4D412FA17AB409D8BB58C07AF1CB36EA816FDF4399A14FDF595D4FDF6F534FD0B7E2B42E7420DA867A19261B78AFED41D697B10D4705152F2354089596E3EA3AAD9C3755D027903844E813AEA6EDB1C09367DB721E7CABEDB3AEABEDBF07A9BBE1B9542D9755345DD7313976DC4455209CC944A79C95012F35EE9606F74055987741D4D9FCAFE747D59F4C35856C18169D7FECE0BCFA42FDA691BEC8EAE20EB91AE63AE0FF4EE342ABDA0AFA5D10FFA8AE628303E182A2C988A1A4498BA5A5C62588F88A5AA43AC97E8720652328F31E32C5085D5E6A8843452AA0CEA832AD674025E4F0ADD81B5A08EC18AE628A8BBD676A9ED8A7E0250EC8A2985BA622A182C7BE08E055CFD403D991000AA5A21A2C5C0A46BA33E69DB0CD8275D41D6276D6C32ED54D19DBA23F37D5FB3E7EBF67B78AFA78ECFEC7991CDD37D40D5A34E8F8A64DE8CA9843E23E26EE003A6D0823D1AEFDA490EBD47EC500C8609E6A40646AACF5DCDA0CEA94A35DE12FD076A27D04A7550F61E349D78593F74699AE69004108FE13404C5C9DA8518CAECC22041CCF3117383E34FBAEDA8646758496BFEFCCA41919E48ED89C31D36E9E4B9006114B587228AE4505D83D09D94DDC92186C428A8A1899F0187038E63468450A63E552F194DCC6240DE9099B46A185A33953D51CC327B02D47148091A6EA012820B86488EF272CBA23DE124C92A014A99A4B5545F1F538311CA1484911A9C6A48406970A27416223D51A0548D831385B1840944694B8313A55568F534692A8E4B125A591728D21406274827EA8D88D2571E9E30563B9BEBD64C278293EDC8D26471C30D1BD085550651FBC10379CE80E1EBB2A10551E0454B6ADD546123759B6B20979764C67559BFFCD017665BA68085D542A14C559AF14B935A0DC6F8E390024EC824218641F6A6C0FC2081A0B9A0F026457B8FA6A502E03333183F8C490A3E9F8E8412CAB43B831142BC96D9114271D9E2AF1D189DD52C93CC0C42A4B18F6ECAD4284A9B98D541CDDD2A36C5B14C9AFF4341100B5B872B31C6B67440992D7414805360841BFEE01AA5367F839C0286391F7CEDDEB2F6FCBD330F477A911C8C484C7202733AC9731A0C452AE07E9C07A5BAEDB6279818792FBB5432BC67B1BF541AE7864513640E8CDA262C3DA086AAB886AFC198DCAB87210F739D6E461F45E08D2CE0081EDA0C4904DEB91B1C70CC438A039F73A43E07BBDD49EF42108A4CCC21D3944E74A3A0E7DA5992CA8A95C6E7210545C29342237A14B587933A139243755E5686ED0D765E1E931060BC99841AFAD8346128B4A3D06E10CD470D1144BF26A67D57E44D0420A40A18BE2EF00ABC1591DE8840B721A3DCABBC3D6AE0ECA28476656F8F1A3FAAF603FEB3CA8BE81E5DE6314ACBFAEBDBA3EB2D6EBD46CD5F67A84CEE7B106F31CC0CD5AA730FB4AB739EDDE55D9414875157A52B6EA7E512CF6F1C55D1495125775823C7C52B5496F5DEF047946E71950FEBAF283ECFAEB6D5665BE121A3F5D794390C92202B55FF6F8F049CDF5E6DEA134988216034133C047495BDDF2669BCC3FB6394F2BBA60C047190FD84F0F7662E2BFC13DD3FED207DCE3343402DF97641673768BD4931B0F22A5B468FC80537CC7E17E83E5A3DE1EF8F494CE49A0C887E2258B2BF3D4BA2FB225A972D8CBE3DFE13F370BCFEF1EFFF1FA8E282722C1B0300 , N'6.1.3-40302')
