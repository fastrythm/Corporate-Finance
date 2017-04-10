﻿CREATE TABLE [dbo].[UserPermission] (
    [Id] [bigint] NOT NULL IDENTITY,
    [UserID] [nvarchar](128),
    [PermissionID] [bigint] NOT NULL,
    CONSTRAINT [PK_dbo.UserPermission] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_UserID] ON [dbo].[UserPermission]([UserID])
ALTER TABLE [dbo].[UserPermission] ADD CONSTRAINT [FK_dbo.UserPermission_dbo.AspNetUsers_UserID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[AspNetUsers] ([Id])
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201704051346104_UserPermissionTable', N'CorporateAndFinance.Data.Migrations.Configuration',  0x1F8B0800000000000400ED3DD96E1C3992EF0BEC3F14EA6977D1A3B2E4E9468F21CD4096AC6E632CB760B91B8B7931D25594947056664D6656B785C57ED93EEC27ED2F6CDEC9237804C93C4A16FAA1AD221924238311C1601CFFF73FFF7BFAB7AFDB68F13B49B33089CF96C7472F960B12AF934D18DF9F2DF7F9DD9F7E5CFEEDAFFFFA2FA76F36DBAF8BDFDA7E2FCB7EC5C8383B5B3EE4F9EED56A95AD1FC836C88EB6E13A4DB2E42E3F5A27DB55B04956272F5EFC65757CBC22058865016BB138FDB08FF3704BAA3F8A3F2F92784D76F93E88AE930D89B2E6F7A2E5B682BA781F6C49B60BD6E46C7991A4BB240D72721E6FAEC23828461E5D0679B05C9C476150ACE7964477CB4510C7491EE4C56A5FFD9A91DB3C4DE2FBDB5DF143107D7CDC91A2DF5D1065A4D9C5ABBEBBE9865E9C941B5AF5035B50EB7D96275B24C0E3970D8656FC702B3C2F3B0C16387C53E03A7F2C775DE1F16C79BED9A424CB960B7EAE5717515AF683B15CFC468EAA0F74D440F86E21EBF75D472B054995FF155DF751BE4FC9594CF6791A44DF2D6EF69FA370FD77F2F831F942E2B3781F45F4BA8B95176DCC0FC54F3769B22369FEF881DCB1BB797BB95CACD8E12B7E7C375A1C5A6FFB6D9CFFF0E7E5E27DB194E073443A22A150749B17DBFB89C4A4DCF5E626C8739216DFF8ED8654681616C14D59AC9BA4C50923FDA43FEDC30D30A71ACE45F11DF4EB56C368B67FDC42294E4971EC978BEBE0EB3B12DFE70F054338F971B9B80ABF924DFB4B03F9D7382CB84431284FF7043DF375F2398C8862DEEF5F68A6359AE65D106FA2301E7EA2ABE0EBE073FC23DC951F5D31CF899779DE66972422056DB733BD4E92880431FA23DF2669FE4BBA212945A52F4F2C28BD1016E9A30B7E6B1AD5ACB638DF4E846232C945C521069E23252567FA256E272AA423F958485B34E6DF05595EF0FBF02EEC89C110D8E9AA973A4A59F43A88BFBC4E0BB9F1E0208E7A20B39048FD726C84123B7A2CB954CEEA2A4FEA5597FF56D0F875C92A9D89BC9E4AC30FBD1CA79B872426EFF7DBCF3D1F1BECE426711EACF39B824E9378E8C9DE6C83301A7A1247B9683245A7CC3AEB303AB178BECEC3DF89AB543C3CF6ECC89867C3926D99F1986C58C33CBD9C982745C905B0F7C1EFE17D857DA92425057FF840A2AA53F610EE6A2B41459E9FD85E5769B2FD9044CD68A6F1D36DB24FD725DA12598F8F417A4F72F3155E24DB5D103F9630142B647B712BA41BE115323DA0151A73030A920353A0A0CC823750EBB16111DCF0B1384533ADA319C18BDA87505835B27CBD2E2F7BE3685BCD64E524CF1CD723C7F5C1C75A2E25E7632DA743725A70714D9B747D50BBB044B093E52ACBC137491656EB542D99C70B354858BEB4AF2035B4031CC4DCC7825164C52507BB33769C76737477D3FD31637CC84977193927F9E8201B1D859446291EC8467DFB90A4F918FA78832456EAD909D0069446A6F931C15C926C9D86BBFA8D4A8EA1E3936701EAF1426025AB240C507F3330586AF9BF282CF992D18299EED26553BD748BA7BBE2B7106705D30CE2FC260AD6645B68DEBA4D4003A06D88FD141B013A63B7F26B46D28B20DDBCF9BA2371A6F9166267710B7C1FE9F2858E564BAF808526EBEE7B4A16DD7650AFB8EBE543D2F744E82EF37B587392FEFDAA1CF40016C861DD94DF91FB203ADFE7855650CCE94925998F84BDA45E3FAD8562FD8DA20F641BA45F5426793F6B2E9F6CF7CA69CCD433E42E9BEDA9DC26FCECAF9DA8E4573CF51A8D3C196B8927F8259E6F4BEB4B4772641D6E8368B9B8498B7F35CE68C519BA5D07E577F941FBD01446A4600C0FFECFA40F8B025EE9D1581720FDC8527E09BA87930413A0CD448609EBB293622098B1E4D86F24DE2469BF08576BAB0F17323FB2D5D7F5CE9BEB527946BD48C437F10686A361AB653FF525FEE47B33672FF4076DC9EB03BD6E2D7BB6A39B41E7A8CFCBB0DB98BB3D81E719A0B4E23B69AFB9460304196636CA499C89DBB596653CA8590832480460A5185E8CF81361909A68CDA0662674BE01DBA4928B50079B27314EEB95F703F45E4567FCEB9AA1B96F2086C8DBA3706C14634E037775BE2BEE0CEBEAD7B28FFA2B693B0B9F4A3FC2D34DC5CB056516ECDC8D917BE3843D20E10DCAC25FFF2A4C33DDBBD9404F7625E31BC5325748C08DD243E598B73CD550C7F5D09D5328CEECAE7807246E2F93F55EF53605C80F6A082879819EC07389B2FB202F545CA74F34D7EF3722EF2548254557ACFA00C8CBA1D420C5A7900B6247B1DA7E592FE2B505363331DB2ECB4DDCD250C67BC4F226ECDDAD7E2D02A691F2EDECB78F594EB6A3C87B4FA18CDFA2F01A566629EE8B2A1167C52A3951E2C0273948B36092DC9A6C382400624CC35239B53367F361F4FF40D621D98DE4C66EE624E0870DBE0EA3C88FBF3C363000F9588CE6CBB7C5C8AC6254EECC99F2239E045963B95FDC048F2557BD24B9E69EEA65B686A9FC4C82CDD047EA220A89A867D9EA67CF2A81914AE07CF3E3EF4C069744ECD555B970A557A5B493F4C22A77C074525E3C682DB352575CF494437BF92A963C89402927F6A5CC584C7D4DCAA93F86B99B9972CC38836F87ABA3AC7A962C52C6D9CD5CCF8D5924F780E5921E8D85340B86F9565445B48CF2AD4AD51AC8BAF32D3C1FBDD9EEA2E4918C9456C6FD85C7F01E1ADEC781E61EEA2BF46C17A4796DAC1E782A6F9C5CF79AE767B9D52C85A67B17A65BF735DF0459F647926E7E0E329567B59FA5DF92F5BE0CADB8CD83ED6EF0D9CCD23A799FCBDBA7F9F8477215AC0B9DF44D5C8E7286F72E597F49F679E3AFFA6BBEC6BAAC7600BC2CE77CBD265976551033D95CD0B61F3BC37B2985359CDDF253CBAFBB51106E617588F79469BBCABD6FEA1E822224E986BDDEBE4BEE4349343E3F43DB55BED4BA8776A94D37EC524B60662B6D7ACA175A75D0AEB3EE656B30305B2AD55BBEDCAE9376C97D4FFF91987D8F3A8C5250E0A936D8B44177C0BEC097836E48BA0DB34C9A3F82ED032E926B06D7C9F719C659C0B34B9E8E323CFB0EB4B68FEABB96AC477565BAEEF2559F67BBF7243F6A471FD570AFD20266A16C7C3912C07EB7301EDC5FA84E4C2F542F8F3FDFBDFCF1FB1F82CDCB1FFE4C5E7E3FFEE54A26D33C5B9F5497393F8A4EF5B534262A8F33FD16447BDF5359117F25CCFC137F0576FEC4DF12978D19760A0B4385D6E2B7DF43B50FA727E5BF99A8C0C61C48B5140CFE29B5843A7F422D572912AA579A6EA7189BA6C7394BB8D726E724581494599850A9F5583F3BF1F1B287F1F2F434A37C7D152898FB1B8D49A602646A1D597E0238018F73E812EADA093E1741F7526BD6D65F0E1DB95B0F68160CCEF91A332A2B1BEC79A6FF2A6877423BD246DA2B200297D9342C830284A49F7EF23DB7D06641EDC0BA1CF33FD360C60B0C40A49F1EC1F9D5B7ABA649AAC4E153E44AE49D3EA7AEEBF9A39C49FD1C410AE0DC4E21B534C783C8413AC0B348EDC0CBB19CDA29B971169EC8217A4DC2B1FCF10776551FD3DDFFA220B6FB24D5A513F6555FE75B7199B3145C2E19D00DC5179835DD4E8235A4E322B31A10F39052CD62ACE4123576AC079F619C1D007DB1396D586F91CAC964B7311A2A3D483F059B82AAE197D5BAED534F87FD81E19A84C3C1B73B1D841A98C331A801CCE210D44BB13902FDC8A775008C84B1AF77A4704D0CD50A8BC8E46D703FC2ABAD31273496A5DDF976E301BC4945C6232CAF731BE59B9BF6F2B6994788709B3A007F3133493930DAD91FE809CE93963CE91BC6EBC7299566E3037543F2FCF1A2724AB63E551D8C591CAD6E3536E78B193CD6211BF3223A636348F3F7DCE2D93DDDA57DBC1DCFE13A8E7695F1EF2673182E3287122536B2337F790EE07B26FD793F35DD7A35536C15344DA00B46D93CCFB2641D56AB6AD62A16B56537F926DE2CB4156E6BC4B6951B0BDC16B41796CEC4C5128A8F2A604E05B4BB5FB340EB761EF47F08A00BE2246989A3A00C6CCA0AD20FE35CA4E4305E87BB20D26D8D1B081E02B8126CF90DBA69F8964BB22B5342C6B90E0B6EF377D370475487A3D31545276AF2010B8CC93EB6BADA58FFBD3B0F14733A5217DD14404364EA8996549B34F99C5297311445A9F0E1BC8AF1E98A2E896248026051389F340616601126A06B7D8D416FC0B627A33A004387417BCA747372F230CB974A93489FB40643866639EEA079FAAC9D03D1A2090ACC68409EEB1A499226D8F2B1A411285391F548462D2629907A4A119216EAC8C468222549F273BC383A12A9DF8A1AF52B1A9512F55FE2C0A850CCCFA2A30D45B2169106313CD120619603913B519F74CF265F5B9EB7CA8AF4A4E8715FCBB872598C7C3590968A08588F325951FAA29F45AC3C34B43C966E7E2A692CC5D3817041B3AA2F328241968031A71C1595220B71418782AAFB3808C5A2F0624229FA1A58280246A1D0DFFA46A067499A11192DE9728EF4C423244A33D0F7B4F300440AE4175092A8036E9ABC26A66BE6939C0C851B2E4B8A04374DF8F950B8A933A9982E994BAB321466D8BC2C12C4D4AF3843E1A54FDB62BA682087CB50F81193C01828E4F6B81102F4947A3D18ADC72D0FB6E669EFAB60FE1907242371C04772A9D62A0DEB623141071BE29021CD73E34674D69727C97A4C842A18B68FBB2E49D06D7A519A8D1837BF292112060DC58686B83B593D3A203479D520EF0F0F7353D74D363FC5F3839B4E3EFD0304151E6F625F930D40D09FE6A44A6770B6AFD9E2A5CB3E60B46421C9809F1329497360A99C381D41709BE39F3D10230774E8E401C01A5AD007F79AB92118909D41E03138551F9D3F24256A1181A0060FBE35A638F3B2AAE9E89489F743D20F18C83718B5C2A186E06C4C38FB98340B616416640B21EF502897DF8BC6D550E72566E56A38BD7F98746F269FD197AFA1AD5BD8A4F4C30768C93EB4345AABFFCC6D9CA5B985441A064AD14E1739A6234A2BC291ACC0E8F44BE39151A42341ACFB0A46201EC0F35AF6A9556ED8A2E518B21A6B2E530A27EE014CD3B5277881BFBCC01E493B81D0443135214C55AF2A999B10C6502CA08964C81A4F787E63E504B7246F4D509B4D4AB2AA5E4FEF85DEDA8BEA3600392C0CC6235D04437B841B409281D00E66A4950883911646A042703BDD0DD16C39B437AC1418ED78AA050BD8BB20C0805949031A48512EC0150D7AC6EB552F130188AA26AC00D8BB4F6A000B961B11AA60253104A9826504043C96BC19570347786B8580020FB208B06DED0825D8E62D1301B629F4A0845A735B93EFA138D38CE5C700D60D5DA90084463FED98F32D2A83998A7FF5B60173D06C8E191574E622A79BA05615249CB2D3B13450BA6C1F028856F7D3EE732361AF1B3D717431B620082A20D9907275540B522CA50FB0E2908DF75A50FD28D9A8080A13AF55605858B7994E080B9A9F49201807A65F371F79C8EED7001760F012800E7D9093E4B906B8C052BBA1B8870231CAC82601DA23886977DC302A871643D2701DD5CEA0801D576C41313A22CE98CD79C09C327C04C49E79C009B75BA3901366C794EAA4C4A35190090899DAAB333215110F00224DE32398AD1A444850DB14353B05160D6222CCBE8D03E6802AAA72C4699E1CC1CDC95F1D01B499E14BFECC88F8126E4717B8B8280FAEC6CF4276B8E44E166E8756EE4941C105F6E88C4233B768009716FED4CCE6711ED5182C584CA33CDD341E9CF12DAB0E2822D8C4C197D9AAC6C5973B8A6AB469DC782958D0D5D23B96DACBA51E4B90ABAF72679CB3AF139638875E0996DACD78C75273EBD02309F0F955EE8BF5FA754211EBD92BC150B311EF08A2A49A1E49120760E5EE4417602764896EBE2622DA56F9A01D7C653A87D40958D40B2037607EF9467704A9E7AF0EB7968810EA35C0B8503A030B1B90B903737B60AC4A1A9CC81C8007408B41254BFD81C2A85CE6CEAD4E076C32B54BE9BAAABCB0A3942C236757874BFBA4AA94D4ED52813FC47D49EBA4698DB7C96E4B903BA60E59B0DFA67C5F82E7A61B9A04674D5311628F20454D1129AE0CDD10A17DEA1D110DAD8638E06A9324B5E7A1D0CABE46186356EE3867B27FD075CE237E416739183EBB7F672C8BDE5AB2870243B3B8DCA90BFF5030B2315C480D2D6242E99C64E49E442DBF7BBC52E041E6904463817A4973C60194BA504483CECDC6D4D106B8C881973853BF1A4F17C336AF62E74DD3B59DAE6ED70F641B343F9CAE8A2E6BB2CBF741546510CEDA86EB60B70BE3FBAC1FD9FCB2B8DD15CA4CF1D5FE74BB5C7CDD467176B67CC8F3DDABD52AAB406747DB2E1FE83AD9AE824DB23A79F1E22FABE3E3D5B686B15A33F8E67D7FBA99F2240DEE09D75ABF795E8569965F0679F03928BD122E365BA09BC47748F298D94ECBBB07895FB27DDE6C4794FFE6FC95A8ACCBE52A3B8F220E588FD7AB62ABA58A58ED9A5084201D598C2D93E906299F87B71E50FAB05D24D17E1B0B3FF3C42987D5B8B6AD090F8D693087D7664DA741C119D80D76780C6E1003E83AF91C964FD23498F6377328EF8278131534C7C2E97F35877455A678A581543F988FFF47B8AB1FFE6918DD8FE670A85CCB3424EA6773585432771A16F533867CF6715E4A36967E9A1F116BCAAB8CDBCC7AEA9F106B09737E21A198615C09A1CF22CD80E97FC6D0209D479AA543BA458478BAE23810CFEF5602C3E3E410CF418DF8ABC66FC382C552DE96782EAB1A2CC3793F8667696C8BF9576C1DD779584828D5CCE5BF3948D4EF5868225BA17F378776F390C4A42D5A4783631A301CA190F7EBFCA6D0A212FE1CB14DE630DF6C83306261353F8DC7C93B61E628E1FAFAE42C236F7F7DE656C6DCCA239BB2645038D6E4CE48441682651ECFE4E785FC34B60B0B2AA46302F0C4A81C2D67D54C701FCBAA95717F5A98127893085FEF4AC1F97A5D6AB890D0E49AD030EB423100C4BAE1F99C4F72CEE16707FB436E7FC0D187DBCB4174173BB70FC5ED520443FD8C6631D0F1E39AD030C5E3C73498C363EA56D1F09886E7E33C8DD856FB99DB9F6B2A76CEFA84AB6068A8B71F2A39F56C876984FA3B721F44E7FBBC38F9C5CE459600B54F77EE2E05B3D425D22A55D7BE8B3E906D907EE1AE8F7C1BCE5EB6E7A0B5BF618CC9D5C49CD9B6FF150FA9CD6107C193E5B7D3423D01E19DD84052ACEF04BFBEB6C01EA3AF35BF212C1361446E82FC81334F74BFCE887B1A39FD58F14F3144D886831A4051D8ADF8C122BF03BB987F692869333D81495267D50E7C3CE5F8E4F5FEB4169FEF1F65E13C91AFF7BF22EC92F14604D4FD88E04E657D4941F5EC7F45595F1BDAF9202C8C6F43D30404926AC09E03111CFDFBB3466BC4934D9C592D18B2905B01CF8DF520A66193900A8017FDF2D5D9AECB3FB38479E5F3D142AB3BDEB51C27E5C646A7F145A4FD38D8FCC2B762B4E034032C44D4CF3852036E96011ED24F657E296E97ED6FE3BE5FFAF18C99A746F6CC66B4A1FB4EECA6CB3FE4C276E440F4CCA21D2B6743748F29A5B09F4B54BB1B9109B12D7888B78F594EB672B874FB342E61CF8CC11363308811B2E00A7CFE303C4BD0425069DED4404809E79A715A7D5B6510028A3CBFCE66DF0F641D921DA825714DD319B75F8751249A1DFA5FC73684BECD6E8388641F481400EC836D33874AC5B7889B151AD12665D0968C72C00B1ECB637649724149E49A100A677D847E26018746A601C187A31092DEDDAFD3DE9B9FE58D5779E359D0384818BC68F1C3FE3D99888A79018782EE571C24F0BE4DFD8E83764DCA511FC39CBF520A8DCF77CB033FD2BEE2A5B854AFF853AD852025079E0E70CF3DB3342CBDD9EEA2E491809EF77CDBD846A2429D0CEFE300D433FB060CBC5D90E6750264165CFFFB34CCC18761AE1A50282F7761BAE517C5B761B4C22CFB2349373F57E95E59A5906E41DCF1C97A5FBAD4DCE6C176C7DDF3D9A6E98247A861128CC23D10B7813F92AB42DF4FD23771F039E2A18BAD08DE90ACBF24FBBC79FEFD355F734C426CB6800DAC996F43F91C932CBB2A48946C2E800B9CD88C53A1444ED9FF3A1B1109E48DF3222FC52CE6788969006318995929BB1B400146DE1A8B25031A70FF3312D66F41B4878035BFCF92A6A489001D69AA4E61EF46531218C352453569F1DBEFA1606FE69A1072A319F377C24500330DB3A40F590E4447F2A832BBBB51070C426A0E2B7AF3C4D1FE3614E399D250E2373084AEFA60692FC10688508340AB898DC39F2FDF9AA7EE82E83505C3B76EE0B8A172557A3B8C3D50CBF3A80230A0D2E67CF8FA75F3B0D896D9908061B63BB790DCAECC8D8D078501140D2BA4072B4275E92E6876EB350CD8FD0115F7A4383DED69F201BA911F5D07C9890295800C08841AAFA015AED7B4A4482D46A44AA171FA77E5E6D15784C934A03C114828F544A09BC67EEFF7EDCFD0E634841EDDE896E787A8F119659F52D20F6F94E4CB34E186D2A13ABA125892A238AD1C9A6815C5BE1D959CAA211B51C6760D3878BFEE3630BCAE6136B4D46639F542484D553F3C19C906CA905CF7E749A8FF755402F2CC786FD270CD0BABFA2704ABDD06F7FC2DBDFE092F0070077532DD71E32FFDC0C62EE300344CAE9BF9F08C76275E40D9424AF501AC2FAFF9CC93FDCF4F58AAD3E54ABD10725FE1144FCD8AB1725B4733443475500DD3F1D543BBBAF87203F6A7747B32487FCBAA3B9B47DDEB4395E32315EE81CACDB86A2EB786FE4C6C9279F65B9D4BAAFAE1FDFBBAA1689F8432B13ECF17941501450C1A7D3F1E28F43D4B5C768BB15E67530CC0729DE8755D24F1A632E32EDE66EFF75174B6BC0BA28C4F0BA8DD3D5F8EC09998DA7A8116C4D40E453B23187C24B670E27C89892DCA68B84ECC7BF84112555349C382A69A91580F06832FC594999C2F4131252C0D9789F3D2F8466992AA68694197D468D3B816838FDD97E09C3D5DF64BC5F33A6C0CCF41D0175F38D488ACC44108AF1FE02BF1F01CB51A5FC4232C0B47DEF64E4CEE9433163B02EA7E5AB025000A366393C1D997D6479D98CCF4EBC41D071F29AAE64B81F28AA6E66E0BC060642201E033EA6AA11A7E3DB583E1275BAE8158AF9D64B44F9CE04E6B5AE438531D542910694C6A8779B814C88B0C5ADB0128801E589ABC6AE193BD0B6850A8A740A10E23DFA533DE35BF747F7775189B1A884C71C60A3B65A9C50A2B59538F912F8A5877592EDA9884B3659DACA726E3DB7F46753E85BEC37510877724CB3F265F487CB63C79717CB25C9C476190D555339B728FAFD6FB2C4FB6411C27795353D3A0FEE3F1CBB2FE23D96C57FC707C15C9124A966D22A0866479805B190CD54D3CFD3B1168A1A5910FE48E1D0A311D7EFC292FF9FBA1E55ACE969FC3FBB0C472C523EA0CDC39D9DC04794ED2B8A7B1E5A224C332A6AE23C5957222A600633DD53E0EFFB9276105F12E2C757824CCF67D955D39124857A4AA8612FF1EA4EB87A058CC75F0F51D89EFF387B3E5F1C98F68B86D60710D1682FAFD0B1A689E8A415A3CCCBE46A34FA855A92F9F00BB0A8E72A02768A0D40357FBBDF11F9B7AC4AE81D8504C57C051413178A435051DBDC2AC2B3CFA05D9BFEAD5704BFFA73C2C1F7B9068641FF450C0E84722255395954934E3ABAA0A487AD6CA8E1E96BBB67EBFEC2CBCE87AF536DE90AF67CBFFAA46BD5ABCFDCF4FF5C0EF16D5A178B578B1F86FFCDC54994696D4FE6D1B7CFD772C81D1751ABD522E1374EFF74CB0951BBDC26E122E788549317C4F102D84A819CF6F03D1EC59FEE1312C5B5665CBA486674F107370A4B86F8F34A4450CCD28441921A227146EF8B0F442595164570503D9D64171136F538A56850A81BDE6B0A517FD727FBA08E3F329F771CA1D4EB8C3E9F67837D7B27C9B3B355510D1B302C71446743A684C49440FFA30E31CABDA7569FE7A3E5D46A74B56451075CE54E5028D4F1C0BE4DB91AA501D43BF778701CFCD2565A9B12677BEBAA1DF25B6D50E8DD9A4A111B7AD7DE877B57C0D44BD0C42403D1968B5277E57DB4602346445D6E13688CAD78EE25F59F56C715C9C81F299A968FE017DE5EFEA25BA9C32049FD5D41A34E5B40625054D782D0866586E0BF938A0EF10221027AEEBE5C1644E62C48706E3E561A1AF8FE82816BAFA887238463CAAAB8EA87883F9DEE2DBB34512F1CC0A436283CE4157531C6A1B07A51BABFDDBCC18B6DEB14BCFADF58CD337AB3617E4C843E2CC233DF17D9F6CF2E02F8BF0F7AF1C6678DDC8E01B3563D9CF33B04EE5A64AB96950839C13D6F062A39450F9EA3DDB9CFAF4F57EEFA76D9DC406AA08D2EDA9D08F616C082796D9E86CB3E04616C71F2E35886503F292821876404319DA7A3537A9EAE536C5563CF4CCBBA0B2877EB9981787AA6FF0342B9DFBCD8EB2D6AF5D7F8E0110C36BDA6DB01ADA18D20F763AB65EAEE95C9542AFAF51A6966B8BE3DA572FF46B18B6375E1A3208B6BCA13D9710D2B70C6220F76D71E62A1DFA05CE143DF4FBACDA5540F463221F550518CD277916824B6A83FBE4D5DCAB37FB3A5C90E1F8719C307591A28767A8EA8A2DFAE58274E145FF9099FA8BB3F3367ACA2C014A0660613213C18C603D53965734E31150489D8E37402872BCD41D9EA98BAFD8E8F5D0B2262A7F2A785FCAD18B13575FCBD103382F4C068C2CB05A0E5FC7D17E4D6CF5460F4BE36A377A80A808237185E705856291467B58405546B76769BE1AA3FDD280F28BF606A8BEF022FA831A0B204D66C03144D0703AA920E5AC8E0255FAD017B4A6F6A1077087ACFC6812099A5F916CE80F2610776585AD86E88313D3D5109F29469112C48C6064D9323CD2563B8567DA1A886467481090B8E0B28858AC90833102BD4AB3BD21CC3FF6E133DCF0C332023DBB730EF4AC39532BCE61783BA98A448EA12B8F737AD1C8E70F981BAFBF616A5622FC186C8296E1F28EE8E0657915475410330D6668C71545E4B4396BF5104EECE5E5DBEB53AF0D1D498B34A24949598811454D1CA46F85A084222B8EB435F0EB3C5322D2F7C33F532BD2AB317A60E78A01DD4DD8C2927E613F9D8736732608D66634647BD2226F068C8E1A3BA025D1D2020BCBB9AED0A39BC19AA9F1880165FC51A14A89669F54561D51FF41FB9107F139E50CCAD2CE57155F3462A727F8FB675D96D187011B3E762612BE1B8910EF085D8C2F8C68AA7941E5104DF42CC8CF7AF41C5536163FACE01BDD38F1FAF120249FA47AA119DD29CA15EA898F193C2C050EA8888DA4A81F8A37B107CDD1B715F6A014517989C183F2A4F2FCE44FA5576F2628AFD89FFA246E7C81C8326DFDA2C46193F7B1594A99BCFCA8FEE17A1FE561F9DA534C57EC51D8100BA0C974CB83697F6681FD8700AC49D19D8741E9CC93E569108AE5190BE5295E87BB20A217CE7502BF399C78B0C46607926FB924BB324832CEC53DBACDD801E6685087012685BEFAD3730ECFE55AE45FBF7DB9A2BF5CF79B390DD019220150104979A20249A1259501CE9116A4D9306D661D9F1EFABC5F63510595F30C0048B73E2D0A91E57A9B2D9DB4A1299FC4285B15AD74C901D8AFDBFF8CA11821D41806DA370F4433B25A5B924FA7AE2785A01C75A8B5E5FC23500F17D0F9499A33C281725E1C1DA988878F6CA5810A6D4F8A6C3405BBE64E33624932DD2706BF2D86D14C4F2BB29AA3E0C75297DA1C944ED4538F2B97C45A89A34825213F150D526C7C52AC45577B72AEBC855F372590BA548C72F271FAE2660A4E9F9E122650AA7D1082B2FAB066E5332DD41D49B24EA7658C4065BC436CE5F92F272B3E1490FEF2421B4AF711633C68D840EB2034A50C75947C4BB822A1310969825B24939A55DB9E8080AAD882A909A80ED4901150D3FA14090888503934022A373535FD54C67919F9D48D4F917AA002B687453CADAE3F0501A1AE76874B30A86BDF6C08850A33F9042431188D3E00BB34F3FB93A21284DD7A7AEAE81DFAA724102A6E83A711BAE92991892C54454129B3B92E99DB7386239B39187726201DABEBFFB46CA67FC8445872BC3D65CEC56E33C573A69D9566FA07CD5E61317A5D40D0CA013C458D4D26768F5042AABFE949A5FA7738BC7BC4741AED44C4E16BD6F108A272A3036237B5B481F1843274ACEAA2582560FBF6216906EBF6E4C1F94E1BC96BBB82E9E888F2629E8294E840560964A6CBD325286948EF8C698A27A6017D7BA7F3EB1CD3BB174B89937EFD3A82AE0BB492738FB603F3DDBA1FCD69A00913A4C1B43F0DC317C09D493E853CB81445015028A4D58C23100093E2A8D4ABE4E79F8996801E64A0C7980378E8914781483EDA14AF3C423AAB11A8A50E0F29C6E4C5089252618C55EEDECB200F3E0742D18F7AD42DC95B33DD6693922C5B2EFA6893D6D4D6B6DCAE1FC836385B6E3E270501D4F12A5DA3403B2C783A324498816E8426A1DB0DE691CC2087AD85CA883E0138D30ACDC174309B4A3E8D720A53F0B403BE6C22BA8F624ABA9B7672C060064C0FF482170074D42C4134EE0AF38B5DA0C9C55EC69B57EE59B755C43CBDBFBC62BEBE937ADEBE9F667EC1D4254C2EF48066163A194EAB984F3D917606E1C941E4947C0F906366BBF724AF65B86E4AC0874B9814E8A39EB6F157C3CCDDB8FF28E76EFAA8E76E5C9D3073D72A8572EABA8B7AE6DA47C6888AA41C986995D2922127E6DF27C1D9E80EB209E93EE672ACB71CA9E459DF4B23D7FA8EE64B60CC0DAA55301D350B61FAEAD6D2DD8CC4D9BB2670BEBA3534A0A8F6DA24CCD03640F0EB363DF02A370684BA8D54666FF4ACB44F7F2082A6DA20F854B3E111D71C6FDDD1868F35A529B3AA211B1EBDA0FA518AA222861AB0B4741B8375496104AB0577E324FAED8ADD8AC136C1506060A7FA9061F8FD835AB784D5C9AD49E258502776DE341DEFAADFBA343AD62702448D9B86A050A26D90A10CEA0411621E06CA6D8E576A9B5DC9D455C9685E55E5A048954F3C7214318B00624C231C3D2245A23F5720744AB13D3AC40773053634AFEBE076C07DCC0811CA8833F591D1783479A40DD9EDB582A1BD91E2916216500560C72212CBDF462508176C0E1CE6E546043CE224314200A64CA289980D49AEA1D56684360562A477CB0A12D0EA1D29ED65508F1428426670A430975E01294DAB77A4340AAD1E2740D0C7E028A19575012375A37784F4F10906489104330C82189464B315CDB4FFBD4C224B7DF487DB36A00BAB6C1F769BE7DDCB25FB577AA10F8602D174D361416190713F0E46CA09D2E77A10248DADAB283D859597409466627F0D9C420F91BAC32A108250EE6D9131B66A0F397AEA30007B84FADBFEE02C54EBCE28C780A10BA4ABA147369EB74DF370A4C6666F48627CF5CCF12477F11B0A55800D9D07A5B288E311263AA2C9ACA8868645BC15751C9322EF73056C53E996C52E997B69A8D7DBFDA8D82CFB7E508D6B7F72DE22E05504EC52E77BA4F03E026E0AD02D6194FB469B47B57394E9DA4E57F5FB42F343F1679EA4C13DB94E3624CAAA5F4F571FF67199D3B6FEAB2ED8DC81382D60C664CD38E6747DDEC67749EB28C4ADA8EDC2258ABD2679B009F2E03CCDC3BBE2E016CD65F5D930BE5F2EAA0AA3650DE4CF64F336FE659FEFF679B165B2FD1C317454FA19A9E63F5D096B3EFDA54A069DF9D842B1CCB04C03FC4BFC7A1F469B6EDD57401A600988F2A9AA49775D7ECBBC4C7B7DFFD8417A9FC486801AF4757E571FC9761715C0B25FE2DBA0ACFE865F5B417EEFC87DB07EBCE92A82CA80E83F048BF6D3CB30B84F836DD6C0E8C7177F1634BCD97EFDEBFF030A48FC45C6FF0100 , N'6.1.3-40302')

