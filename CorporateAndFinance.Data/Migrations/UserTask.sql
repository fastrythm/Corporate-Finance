﻿CREATE TABLE [dbo].[UserTaskDetails] (
    [UserTaskDetailID] [bigint] NOT NULL IDENTITY,
    [UserTaskID] [bigint] NOT NULL,
    [FromUserID] [uniqueidentifier] NOT NULL,
    [ToUserID] [uniqueidentifier] NOT NULL,
    [Remarks] [nvarchar](max),
    [Status] [nvarchar](max),
    [CreatedOn] [datetime] NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_dbo.UserTaskDetails] PRIMARY KEY ([UserTaskDetailID])
)
CREATE INDEX [IX_UserTaskID] ON [dbo].[UserTaskDetails]([UserTaskID])
CREATE TABLE [dbo].[UserTasks] (
    [UserTaskID] [bigint] NOT NULL IDENTITY,
    [Title] [nvarchar](max),
    [Description] [nvarchar](max),
    [Priority] [int] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [DueDate] [datetime] NOT NULL,
    [CreatedBy] [uniqueidentifier] NOT NULL,
    [CreatedOn] [datetime] NOT NULL,
    [LastModified] [datetime] NOT NULL,
    CONSTRAINT [PK_dbo.UserTasks] PRIMARY KEY ([UserTaskID])
)
ALTER TABLE [dbo].[UserTaskDetails] ADD CONSTRAINT [FK_dbo.UserTaskDetails_dbo.UserTasks_UserTaskID] FOREIGN KEY ([UserTaskID]) REFERENCES [dbo].[UserTasks] ([UserTaskID])
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201705011100469_UserTask', N'CorporateAndFinance.Data.Migrations.Configuration',  0x1F8B0800000000000400ED3DD96E1C3992EF0BEC3F14EA6977D0A3B2E4E9468F21CD4096EC6EA32DB760B91B8B7931D25594947056664D6696DBC260BF6C1FF693F61736EFE4113C82641E250BFDD056910C9291C18860308EFFFB9FFF3DFDFBD76DB4F842D22C4CE2B3E5F1D1B3E582C4EB6413C67767CB7D7EFBE71F977FFFDBBFFFDBE9ABCDF6EBE2F7B6DFF3B25F3132CECE96F779BE7BB15A65EB7BB20DB2A36DB84E932CB9CD8FD6C976156C92D5C9B3677F5D1D1FAF48016259C05A2C4EDFEFE33CDC92EA8FE2CF8B245E935DBE0FA2AB6443A2ACF9BD68B9A9A02EDE055B92ED8235395B5E24E92E49839C9CC79BD7611C14238F2E833C582ECEA33028D67343A2DBE52288E3240FF262B52F7ECBC84D9E26F1DDCDAEF821883E3CEC48D1EF368832D2ECE245DFDD7443CF4ECA0DADFA812DA8F53ECB932D12E0F1F306432B7EB8159E971D060B1CBE2A709D3F94BBAEF078B63CDF6C529265CB053FD78B8B282DFBC1582E7E2347D5073A6A207CB790F5FBAEA39582A4CAFF8AAEFB28DFA7E42C26FB3C0DA2EF16D7FB4F51B8FE853C7C483E93F82CDE4711BDEE62E5451BF343F1D3759AEC489A3FBC27B7EC6EDE5C2E172B76F88A1FDF8D1687D6DB7E13E73FFC65B978572C25F814918E482814DDE4C5F67E22312977BDB90EF29CA4C5377EB321159A8545705316EB266971C2483FE94FFB7003CCA98673517C07FDBAD5309AED1FB7508A53521CFBE5E22AF8FA96C477F97DC1104E7E5C2E5E875FC9A6FDA581FC5B1C165CA21894A77B829EF92AF914464431EFF7CF34D31A4DF336883751180F3FD1EBE0EBE073FC23DC951F5D31CF899779DE64972422056DB733BD4C92880431FA23DF2469FE6BBA212945A5CF4F2C28BD1016E9830B7E6B1AD5ACB638DF4E846232C945C521069E23252567FA356E272AA423F950485B34E6DF06595EF0FBF036EC89C110D8E9AA973A4A59F432883FBF4C0BB971EF208E7A20B39048FD726C84123B7A2CB954CEEA2A4FEA5597FF56D0F855C92A9D89BC9E4AC30FBD1CA7EBFB2426EFF6DB4F3D1F1BECE426711EACF3EB824E9378E8C95E6D83301A7A1247B9683245A7CC3AEB303AB178BECEC32FC4552A1E1E7B7664CCB361C9B6CC784C36AC619E5E4ECCA3A2E402D8BBE04B7857615F2A4949C11FDE93A8EA94DD87BBDA4A5091E747B6D7EB34D9BE4FA26634D3F8F126D9A7EB126D89ACC78720BD23B9F90A2F92ED2E881F4A188A15B2BDB815D28DF00A991ED00A8DB90105C98129505066C11BA8F5D8B0086EF8589CA299D6D18CE045ED4328AC1A59BE5E9797BD71B4AD66B27292278EEB91E3FAE0632D9792F3B196D321392DB8B8A64DBA3EA85D5822D8C97295E5E0EB240BAB75AA96CCE3851A242C5FDA57901ADA010E62EE43C128B2E29283DD193B4EBB39BABBE9FE98313EE4A4BB8C9C937C74908D8E424AA3140F64A3BEB94FD27C0C7DBC41122BF5EC0468034A23D3FC98602E49B64EC35DFD4625C7D0F1C99300F57821B092551206A8BF19182CB5FC5F14967CC968C14C77E9B2A95EBAC5D35DF15B88B3826906717E1D056BB22D346FDD26A001D036C47E8A8D009DB15BF92D23E945906E5E7DDD9138D37C0BB1B3B805BE8F74F94247ABA557C0429375F73D258B6E3BA857DCF5F221E97B227497F93DAC3949FF7E550E7A000BE4B06ECA6FC95D109DEFF3422B28E6F4A492CC47C25E52AF9FD642B1FE46D17BB20DD2CF2A93BC9F35974FB67BE53466EA197297CDF6546E137EF6D74E54F22B9E7A8D469E8CB5C413FC12CFB7A5F5A52339B20EB741B45C5CA7C5BF1A67B4E20CDDAC83F2BBFCA07D680A235230867BFF67D2874501AFF468AC0B907E6429BF04DDC3498209D06622C38475D9493110CC5872EC77126F92B45F84ABB5D5870B991FD9EAEB7AE7CD75A93CA35E24E2AB7803C3D1B0D5B29FFA127FF2BD99B317FA83B6E4F59E5EB7963DDBD1CDA073D4E765D86DCCDD9EC0F30C505AF19DB4D75CA301820C331BE524CEC4ED5ACB321ED42C04192402B0520C2F46FC8930484DB4665033133ADF806D52C945A883CD9318A7F5CAFB017AAFA233FE75CDD0DC371043E4ED5138368A31A781BB3ADF15778675F56BD947FD95B49D854FA51FE1E9A6E2E582320B76EEC6C8BD71C21E90F00665E1AFFF3A4C33DDBBD9404F7625E31BC5325748C08DD243E598B73CD550C7F5D09D5328CEECAE7807246E2F93F55EF53605C80F6A082879819EC07389B2FB202F545CA78F34D7EF3722EF2548254557ACFA00C8CBA1D420C5A7900B6247B1DA7E592FE2B505363331DB2ECB4DDCD250C67BC4F226ECDDAD7E2D02A691F2EDEC370F594EB6A3C87B4FA18CDFA2F01A566629EE8B2A1167C52A3951E2C0273948B36092DC9A6C382400624CC35239B53367F361F47F4FD621D98DE4C66EE624E0870DBE0CA3C88FBF3C363000F9588CE6CB37C5C8AC6254EECC99F2239E045963B95F5C070F2557BD24B9E69EEA65B686A9FC4C82CDD047EA220A89A867D9EA674F2A81914AE07CF3E3EF4C069744ECD555B970A557A5B493F4C22A77C074525E3C682DB352575CF494437BF92A963C89402927F6A5CC584C7D45CAA93F84B99B9972CC38836F87ABA3AC7A962C52C6D9CD5CCF8D5924F780E5921E8D85340B86F9465445B48CF28D4AD51AC8BAF32D3C1FBDDAEEA2E4818C9456C6FD85C7F01E1ADEC581E61EEA2BF46C17A4796DAC1E782A6F9C5CF79AE767B9D52C85A67B1BA65BF7355F0759F647926E7E0E329567B59FA5DF90F5BE0CADB8C983ED6EF0D9CCD23A799FCBDBA7F9F047F23A58173AE9ABB81CE50CEF6DB2FE9CECF3C65FF5B77C8D7559ED007859CEF97A4DB2EC7541CC647341DB7EEC0CEFA514D67076CB4F2DBFEE4641B885D521DE53A6ED2AF7BEA97B088A90A41BF67AFB36B90B25D1F8FC0C6D57F952EB1EDAA536DDB04B2D8199ADB4E9295F68D541BBCEBA97ADC1C06CA9546FF972BB4EDA25F73DFD4762F63DEA304A4181A7DA60D306DD01FB025F0EBA26E936CC3269FE08B60FB848AE195C27DF67186701CF2E793ACAF0EC3BD0DA3EAAEF5AB21ED595E9AACB577D9EEDDE91FCA81D7D54C37D9D16300B65E3F39100F6BB85F1E0FE4275627AA17A7EFCE9F6F98FDFFF106C9EFFF017F2FCFBF12F573299E6D9FAA4BACCF95174AAAFA53151799CE9F720DAFB9ECA8AF82B61E69FF82BB0F327FE96B86CCCB05358182AB416BF7D09D53E9C9E94FF66A2021B7320D55230F8A7D412EAFC09B55CA548A85E69BA9D626C9A1EE72CE15E9B9C93605150666142A5D663FDECC4C7CB1EC6CBD3E38CF2F555A060EE6F3426990A90A97564F909E0043CCEA14BA86B27F85C04DD4BAD595B7F3974E46E3DA0593038E76BCCA8AC6CB0E799FEABA0DD09ED481B69AF80085C66D3B00C0A10927EFAC9F7DC429B05B503EB72CCFF4C83192F3000917E7A04E757DFAE9A26A912874F912B9177FA9CBAAEE78F7226F573042980733B85D4D21C0F2207E900CF22B5032FC7726AA7E4C659782287E83509C7F2C71FD8557D4C77FF8B82D8EE9254974E782093C68744759CCC562EADB6687C58D81C834F615F1530DF32D925B9BBA1640613C2DB09E786B65CC47103621E02B8598C95C8A5C68EF596358C1F07A00A37A70DEB0853F9CFEC364643A507E9A760535035FC685CB77DECE9B03F305C937038F876A78350037338063580591C827A293647A01FF9B80E80919EE1EB892C5C13438DC942FA6E83BB111EA48D39A1B12CEDCEB71B0FE0AD45321E617953DD289F13B5F7D2CD3CA29FDBAC08F83BA7493685D1CEFE50AAF8241700BFCF332F1FA6549A8D0FD435C9F3878BCADFDAFA5475306671B4BAD5D89C2F66F068669CFBA0A0BCDBB6409C26F06CCC5CFF7EE69AB16DA9F97B6EE9013CDDDF7D3CC5CFC104807A2CFD10649FDB1C014E8FA53DA059F0357649B60E212C84311F52CB995DADD4A5D6E981A83F241E80E8D35E788A53D2551DF174A198C341D7BD689734247DD52E1B3FB2342E04FC427D407F7CB0A37346847AFDCE5C6956FCC885138DC983747AD541DA31AAE25033792DB8DC132FEA9D70813A5CC5C5849F759CCA375B83DC7694FCCFC9D9DBBFA3F76138791F4A9E8361ACB84AEA86699AFEBC1F9B6E3D358BAD021D035D30147C9E65C93AAC56D5ACB57ABBEBABCA938CA38357F16641D505A77BF68BAB11DBD61E2F705BD05E5886C3154B283EAA803915D04E216181D6ED3CE83F09A09B57E83C0CCAD0FCAC20FD30CE454A0EE375B80B22DDD6B881E021689FCC85488755370DDF7249766552F338D761C16DFE6E1AEE88EA7074BAA2E8444D3E60895CD9C756D7CBEDBF77E7436D4E47EAB2F10268884C3DD1926A93269F531AF480A228153E9C57313E5DD145FD0C49002C6BEC93C6C01282C20474B5DA31E80DD8F664540760E830684F9930594E1E6619FF6912E9D32E62C8D02C4B33344F9F777E205A344181190DC8ABB52049D2045B3E963402652AF276CAA8C52489674F295C6F3D99184DA424497E8E67474722F55B51A37E45A352A2FE4B1C18158A190675B4A1483728D2208627EAB314BA10B913F549F76CF2B5E59957AD484F8A1EF7B58C2B97C5DC2D06D25291C3C5A34C56146FEB67116B670E2D8FA59B9F4A1A4BF174205CD0AC6EA18C6090450CCD294745A5C852B2D0A1A02A970F42B128BC98508ABE8A2B8A805128F4B7BE11E85992284F464BBAAC793DF108A97E0DF43DED3C00910219B29424EA809B26339FE99AF9347D43E186CBF327C14D93406928DCD4B9004D97CC25061C0A336C66410962EA579CA1F0D2271E345D3490857028FC88690C0D14727BDC082926947A3D986F825B1E6CCDD3DE57C10C8A0E4846E280CF45A05AAB3431018B093A5D060E19B2AC068E44677D7992ACC744A88289A770D72509BA4D2F4AB311E3E6372544CACBA1D8D0107727AB47078426AF1AE4FDE1616EEABAC9E6A7787E70D3C9A77F80A0123C99D8D7640310F4A739A9D2199CED6BB678E9F267192D594893E5E7444A1275592A274E4710DCE6F8670FC4C8011D3A790A1B0D2DE8D3D398B92118909D41EA1C70AA3EBFD49094A84504821A3CF8D698E2CCCBAAA6A35326AD03927EC07C0D83512B9C51029C8D49C83426CD42189905D942C83B14CAE5F7A27135D4798959B91A4EEF1F26DD9BC967F4E56B68EB163629FDF071F8B20F2D0DCAEF3F739B4EC3DC4222CDF641D14E97204047945684235981D1E997A69D41918E04B1EE2B18C969018C265099C4D4A105AC965F8737E15C16D4115922FC36B473308705D5864D4D5F705C14DA00A7C28DFB5246A038C0D75F460B2AC77FF1AD027AA7D05CDF156103033C86D4B10705FE8A8F1693B453419AB0BD2666AFEA5525C01602678A0534B13359137BC16FAC9CE086E4ADD173B3494956D538EDE31E5A0B65DD06208785C1C4408860E8180403483210DAC18C7E24C260F413235021B89DCE2661B61CDAFF5A0A8C7675D682052CAC1060C090A9010D947512E08A2664E3F5AA978900D43B14AB00F60EBB1AC082AD50842AD8E50C41AA601901018F25FF70A08123BCEE434001170004D8B6DE9E126CF37A8E005B3F88ABA1D6DCD6E47B28CE34636B3480754D577703A1D18F89E67C8BCAFAACE25FBD35CA1C349BBC52059D311DE826A8955309A7ECB47A0D942E8DA000A2BD6D68F7B991B0D78D9E38BAE43D20082AD391016130C1B62061D0CAA821401528E3F3A43B4BE039A2B4145648B3718F0BAA1F25B115C191A279010C8FEC36D3A906823E6A1210C981E9D7CD47E0B2FB35C00518C407A0431FEC2779B6040C39D46E289EA6408C32C24F80F60062DA1D378C22A4C590346C4DB5332870CD155B50AC9A883366731E30A70CA302B1671E78C5EDD628F48AD931A5D029F168146C0542A6F6EA8C4C45E40F8048D33821C120A18914A2B629EA9B0A2C1AC406997D1B07CC89AFE90AC4699EDEC1CDC95FDF01B499E14BFEDC8EF8126E4717B84E290FAEC6DF4876B8E4CE466E8756EE5144C105F6E88C42B3F0000097167105CCE6719105182C584CA33CDD341E9CF12DABF32E22D8C4D19DD9AAC6D59D3B8A6AB469DCD92958D085D73B96DA2BAF1E4B90CBBB72679CD3BB139638C7760996DACD78C75273EBD02309F07D57EE8BF57E774211EBE12EC150B311EF08A2A49A1E49124778E5EE445778276489EEEE2622DA56F9A01DDD653A87D4195ED40B2077787EF9467704A907BC0EB79688102AEFC1B8503AC50B1B90B9C5737B606C5D1A9CC81CE107408BDE5BDBE04061542E73276FA7033699DAA574E1565ED8514A9691D3B7C3A57D52554AEA7EACC01FE2BEA47556B6C6DB64B725C82D59872CD87F59BE2FC183D90D4D82D3B2A908B14790A23AA4145786EEB8D03EF50EB98656431C70B54992DAF3506865DF488C312B772035D93FE842EA11BFA0D3280C9FDDBF339645AF45D94381A1595CEEDC887F2818D9182E54C21131A174D263362073D3A396DF3DA929F02073CCA3B140BDEF79D164E1B4B4B03AAB773A131450A5DB19C7A79BA7358D4AAB74340320767B72461794F1544494CE57CAD45B0AB8F782775E53E7284FF7E8361D6BE712D5B59DAE6ED6F7641B343F9CAE8A2E6BB2CBF74154A53DCFDA86AB60B70BE3BBAC1FD9FCB2B8D915BA5F41E47FBE592EBE6EA3383B5BDEE7F9EEC56A9555A0B3A36D9746789D6C57C126599D3C7BF6D7D5F1F16A5BC358AD197CF30E5CDD4C79920677846BAD1FAE5F8769965F0679F029285D4B2E365BA09BC4014CF2F6DB4ECBFB78895FB27D0D6E4794FFE69CCEA85CF1E52A3BB7300E588FD7D7C5564B8DBADA35A108413AB2185B963D0952BE624A3DA07444BC48A2FD36167EE689530E8B29B24A43631ACCE1B535B56850707D2E831D1E831BC400BA4A3E85E50B3E0DA6FDCD1CCADB20DE4405CDB170FA5FCD21BD2E3343D340AA1FCCC7FF23DCD5DE1B348CEE477338549E7A1A12F5B3392CAAD4170D8BFA19433EFB382F1501967E9A1F116BCAABE4F9CC7AEA9F106B09737E21A158194109A14F9BCF80E97FC6D0209D389FA543BA458478BAE23810CFEF5602C3E3E410CF418DF8ABC6CDC582C5522EB3782EAB1A2CC3793F8667696C8BF9576CE35D78584828D5CCE5BF3948D4EF5868225BA17F3787767D9FC4A4ADD64E83631A301CA190F7EBFCBAD0A212FE1CB14DE6305F6D2B47331A56F3D3789CBC13668E12EE4D765E5C4DBF109E91B7BF3E712B636EE5914D5932281C6B726724220BC1328F27F2F3427E1A538F0515D2811D7862548E96B36A26269865D5CA70612D4C09BC4984AF77A5A0A9550A094DAE090DB32EE90940AC1B9ECEF924E71C7EA5B13FE4F6071C7DB8BD1C4477B173735FDC2E4530D4CF6816031D3FAE090D533C7E4C83393CA6121E0D8F69783ACED3886DB55BBEFDB9A60220AD4FB80A86867AFBA19253CF769846A8BF25774174BECFEFABEA8D224B80DAA73B77978259EA126995AA6B76465DC15C1A16DF86B397ED3968ED6F186372353167B6ED7FC5432A1F4744CB34DB86867A02C23BB181A458DF097E7D6D2974465F6B7E435826C2885C07F93D679EE87E9D11F734F291B2E29F629CB70D073580A2B05BF183457E077631FFD250AE777A02935CF0AA1DF878CAF1C9EBFD692D3EDF3FCA7A9B225FEF7F45D825E38D08A8FB11C19DCA82BA82EAD9FF8AB2BE36B4F35E5818DF86A6090824D5803D072238FAF7278DD688279BF8FE5A30642141069E1BEB414CC3262115002FFAE5ABB35D977F6609F3CAA7A3855677BC6B394ECA8D8D4EE38B48FB71B0F9856FC568C169065888A89F71A406DC2C033CA49FCA2461DC2EDBDFC67DBFF4E319334F8DEC89CD68331D38B19B2E89940BDB9103D1338B76AC9C0DD13DA694C27E2E51ED6E4426C4B6E021DE3C6439D9CAE1D2EDD3B8843D31064F8CC120A4CA822BF049E0F02C410B41A579530321259C6BC669F56D71520828F2FC3A9B7DDF93754876A096C4354D67DC7E1946916876E87F1DDB10FA26BB092292BD275100B00FB6CD1C2A150E246E5668449B94415B32CA012F78288F599B4D8D71C1639B100A677D847E26018746A601C187A31092DEDDAFD3DE9B9FE48D5779E359D0384818BC68F1C3FE3D99888A79018782EE571C24F0BE4DFD8E837645CA511FC29CBF520A8D4F77CB033FD2BEE2A5B87CBDF853AD852025079E0E70CF3DB3342CBDDAEEA2E481809EF77CDBD846A2429D0CEFE200D433FB060CBC5D90E675166B165CFFFB34CCC18761AE1A50282FB761BAE517C5B761B4C22CFB2349373F57397B59A5906E41DCF1C97A5FBAD4DCE4C176C7DDF3D9A6E98247A861128CC23D10B7813F92D785BE9FA4AFE2E053C443175B11BC21597F4EF679F3FCFB5BBEE69884D86C011B5833DF86F2392659F6BA2051B2B9002E7062334E85123965FFEB6C44249066CF8BBC1453D1E325A6018C616466A5EC6E000518796B2C960C68C0FDCF4858BF07D11E02D6FC3E4B9A92E64D74A4A9BA0E811B4D49600C4B15D5A4C56F5F42C1DECC3521E44633E617C24500330DB3A40F59CA4847F2A812E1BB51070C426A0E2B7AF3C4D1FE3614E399D250E23730844AC5686B2FC10688508340AB898DC39F2FDF9AC7EE82E83505C3B76EE0B8A6527B7A3B8C3D50CBF3A80230A0D2E67CF8FA75F3B0D896D99080617240B790DCAE56918D078501140D2BA4072B4275E92E6876EB350CD8FD0115F7A4383DED69D227BA911F5DCCCA890295800C08841AAFA015AED7B4A4482D46A44AA171FA77E5E6D15784C934A03C114828F544A09BC67EEFF7EDCFD0A680841EDDE816C4374E14142934E2572ACDE60676B0A24D285210687E7A9C1B5F78F45949FDC80B49CA551309211DAAA36019E18E9D60A0E4DE0DD9887A47D78083F7DB6E03C3EB1A66434B6DA25C2F84D494ABC493916CA00CC9757F9E84FA5F472520CFC2E83A0DD7BC00AF7F42B0DA6D70C75B2EEA9FF0A206775027D3A737FE52326CECB23040C3E4FAAA0F6F7177E2051450A4A6338045EA259F8DB3FFF9114B75BA0EAF1742EE4BF7E2A95931566EFF698688E61FAA01410CF7414142B76D1A2BC0E30CEA301DDF3EB4EBA22FD76B7F4ABDA747806FF96AA02F176069D6A6EA76DB99B555005404D18F8348836DC5915A3956061307AD4C0101112FFD3BC67600C1EA7F1D3FD2C04FD2A0A7835955DCF07A241D0E23FE18FA392C80249F5C761717AC2A6B9870ED6A7E9D461E5EEE09F01CD2FE388D5AFD4D1F62B6328C97834C837470BBC1B9DCB83D179BDF3A87FE4C6CD91CF65B9D4BCA3AE32316BAA1682FCBB25410AF752B4B428B1834FA7E3C50E87B96B8EC1663BDCEA6BC91E53AD1EBBA48E24DF530BD7893BDDB47D1D9F23688323ED1B176F77C812567626A0B465B10533B14ED5E69F091D8CAD9F32526B62AB7E13A311E7E0749544D6D300B9A6A46627D320DBE1453677CBE04C5D430375C26CEEFF41BA5C93EB4D7862EA9D1A691BA061FBB833A7FBAEC978AE775D8A8E483A02FCA41B829E58EF3716E0621FC9881AFC4C373D46A7C118FB02C1C79DBBB65BB53CE58EC0828FC6EC1960028D81C9406675FAC64EFC6AE86E251E23A71C7C147D2CDF952A0BCA4BDB923263018991A09F88CD2D2F5382A53874C7CB4E51A88F5DA4946FB5450EEB4A6458E33D541B58F91C6A47698874B81BC6CB2B51D8002E881A5C9EB303FDABB8006857A0A142A4BF35D3AE35DF34BF7775759BAA9EACC949BAEB053168FAEB0923515A6F932CF7597E5A28DB23C5BD6E9076B32BEF967546788EA3B5C0571784BB2FC43F299C467CB9367C727CBC5791406595D07BC2960FD62BDCFF2641BC471923755C20D2A5A1F3F2F2B5A93CD76C50FC7D7C52EA164D98679E4A42CC3AD0C862A419FFE42045A6869E43DB96587424C871F7FCA4BFE7E68B996B3E5A7F02E2CB15CF188BAA6484E36D7419E9334EE696CB928C9B0CC12D091E24A3911E37C5C4FB58FC37FEE495841BC0D4B1D1E09B3F58E62578E04D295DDACA1C45F82747D1F148BB90ABEBE25F15D7E7FB63C3EF9110DB74D95528385A07EFF8C069AA762D8390FB3AF3AED136A55BCD427C0AE26B51CE8091A28F55CD67E6FFCC7A65CD06A203614D395A456500C1E694D896AAF30EB9AD57E41F6AF7A35DCD27B390FCBC71E241AD9073D1430FA9148C95465859FCDF8AAAAA6A39EB5B2A387E5AE6DD4083B0B2FBA5EBC8937E4EBD9F25FD5A8178B37FFF5B11EF8DDA23A142F16CF16FF8D9F9B2A3CCD92DA7F6C83AFFF892530BAF2B457CA65D208F93D136C2D6AAFB09B14525E61520CDF13440B216AC6F3DBD07A7B967F780CCB9655D932A9E1D913C41C1C29EEDB230D695966330A51C617EA09851B3E2CBD505614D955C140B67550DCC4DB94A255A14260AF396C3169BFDC9F2E2BFD74CA7D9C728713EE70BA3DDECDB52CDFE64E4D9578F6ACC031A59E9D0E1A53E4D9833ECCB8AFAA765D9ABF9E4E97D1E992D545469D33550164E313C702F976A42A5499D9EFDD61C0737349596AACC99DAFD7EC77896D2886319B3434E2B6D59CFDAE96AFEAAC974108A82703ADF6C4EF6ADB38BB86ACC83ADC0651F9DA51FC2BAB9E2D8E8B33503E3315CD3FA0AFFC5D0568975386E0B39AEAC9A69CD6A048B209AF05C10CCB6D211F07F41D4204E2C475BD3C98CC498CF8D060BC3C2CF4159F1DC54257F1590EC7884775F59E156F30DF5B7C7BB6EC339E5961486CD039E8FAD0436DE3A07463B57F9B19C3D63B76E9B9B59E71FA66D5E6821C79489C79A427BEEF934D1EFC6511FEFE95C30CAF1B197CA3662CFB7906D6A9DC5429370D6A9073C21A5E6C9412AA028F679B535F90C7EFFDB4ADFCDC401541BA3D15FA318C0DE1C4321B9D6D16DCC8E2F8C3C593B16C405E2419C30E6828435BAFE62655BDDCA6D81ACE9E791754C8D92F17F3E250F50D9E66A573BFD951D6FAB5EBCF310062784DBB0D56431B43FAC14EC7D6CB359DABBBECF535CAD4726D715CFB7ACC7E0DC3F6C64B4306C1166CB6E7124272B4410CE4BE2DCE5CED66BFC09932CE7E9F55BB9ACE7E4CE4A3AA00A3F924CF4270496D701FBD9A7BF5665F870B321C3F8E13A62E52F4F00C555DF968BF5C902E25ED1F3253517A76DE468F992540C9002C4C66229811AC67E7AA82D1663C020AA9D3F10608458E97BAC33375F135A8BD1E5AD644E54F05EF8B537B71E2EAAB537B00E785C980910556CBE12B53DBAF89AD47ED61695C356A0F10156124AEF0BCA0502C3B6D0F0BA833EDF62CCDD797B65F1A5050DADE00554A0488F1197C506301A4C90C3886081A4E2715A49CD551A08A39FB82D65473F600EE90951F4D2241F32B920DFDC104E2AEACB0F59D7D7062BABEF313C528528298118C2C5B8647DA6AA7F04C5B0391EC0C090212175C16118B15723046A05769B63784F9C73E7C861B7E5846A02777CE819E35676AC5390C6FA772A66B49D9EB3174E5714E2F1AF9FC0173E3F5D74C156E841F834DD0325CB01A1DBC2CAF4B8D0A62A6C10CEDB8A2889C3667AD1EC289BDBC7C7B7DEAB5A12369D9693429294B4BA3A88983F4AD109450C2CC91B6067E9D678A5EFB7EF867AA5F7B35460FEC5C31A0BB095B2ADBF36D4A28992D935F989522D3D49980060A643FB9CA61583F584FDA90D94B0BD31AB0776AEC80F6534BBB332CDDBBE2D46E667AA62E350694F14785AA3B9B7D52594567FD07ED471EC4E794B3654BEB665530DA48889CE0F9525D4ADA87D91E3E76267A4D3712A1D4203450BE98B3A9BE09957036D12E21EFF2D133735949E621C4BD4F93CCCB8783907C928ACB6674A728B1AC273E66F0C0B719A864B35F3574400D77A41BD0A1B8697B504E7D9BB70F4AD7555545367FC9505540367BCC60210C6F116D4BC6A26D19FD602743065D95D913E1F5C5993D0194C416D8B94581E981ECF4B5833C602E47CBE5500D7F9C40093A971B48533B7962FB47572FD99160056DF21B9356F24AC307E5506D796597A189AAB2D24C501A283FF6B95CF93AD165F59A4589C326FD73B394B286C951FDC3D53ECAC3D2E9A398AED8A3B021164093F09E07D3FECC02FB9300AC3181E66150FAF466791A846295E6E22CC7EB701744F4C2B94EE03787F30F97D8EC40F22D976457E64A8873718F6E337680391AD46180A9A4A3FEF45CDC53B916F9D76F1D58E82FD7FD664E0374A268001444529EA840526F51F50EE7480BD2A4D836B38E4F0F7DFACFB1A8824A7D0A00A45B1F1785C852BECE964EDA08D58F62B20D15AD743982D8AFDBFF8CA11821E3080CB46F1E8866642537259F4E5D56124139EA8C2B96F38F403D5C5E878FD2D4510E94F3ECE848453C7C820B1AA8D0F6A8C84653B773EE34235626D57D62F0DB6218CDF4B4222B3D2EB503C82B6E0F4A27EAA9C7954B62C9E451A49290A6920629363E2AD6A22B413D57DEC2AF9B12485D466639F9387D713305A7CF520D1328D53E0841597D58B32ADA16EA8E2467B7D33246A0323E2EA60A009493159F1180FEF2421B4AF711433D69D840EB2034A5CC7820F9967061626312D2C4B84A2685C2A26641405588E1D40454C76BCA08A8697D8C040404AA1E1A01959B9A9A7E2AE3BC8C7CEAC6C7483D501DFBC3229E56D79F82805057BBC32518D4B56F368442459B7E0472198D461F805D9AF9FD515109C26E3D3D75F4717D53120815BEC9D308DDF498C84416B1AAA094D95C97CCED39C391CD1C8C3B13908ED5F57F5A36D33F64222C39DE9E32E762B799E239D3CE4A33FD8366AFB018BD2E2068E5009EA2C62613BB472821E3EFF4A452FD3B1CDE3D623A8D7622E2F035EB780451B9D101291CB4B481F1843274ACEA925948C0F6ED43D20CD6EDC983F39D36A187ED0AA6A3232AE6660A52A2F3594820335D1E2F4149337BCC98A678621AD0B7773ABFCE31BD7BB19438E9D7AF43CABBC86339F7683B30DFADFBD19C069AB8791A4CFBD3307C01DC99E453C8B32DA02800CA0D6035E348D6B632AEA70B44ABA3E6D44E53550012AF64D63FE214562A541002D7360DE63225465249BE933C740AADB8CAC223AD661E8142985C98E56AE4B4C1C4D3404F76D073DD013C05CAE384241F6D8A774021EFE908D452071015630A728E494A657EA88A3C5C0679F02910AAC3D5A36E48DE1A72379B9464D972D1C723B5C6D8B6E5667D4FB6C1D972F3292908A08E68EA1A05DA61C1D3B143C20C74233409DD6E308F6406396C2D544639128033ADD01C4C07B3A9E4D328A730054F8768C826A2FB28A6A4BB6927074CAAC0F4402F78014047CD1244F3BF30BFD8059A5CEC65BC79E59E755B45CCD3475428E6EB3BA9E7EDFB69E6178CA1C2E4420F6866A193E1B48AF9D4136967101EA5444EC9F7003966B67B47F25A86EBA604BCFC8449813EEA691B8F46CCDC8D839872EEA68F7AEEC6190E3377AD5228A7AEBBA867AEBDA88CA848CA819956292D197262FE051B9C8DEE209B90EE632EC77ADBA24A9EF5BD3472ADEF68BE04C620A55A05D351B310A6AF6E2DDDDD599CBD6B02E7AB5B43038A6A2FD6C20C6D0304BF6ED303AFD28941A8DB4865F646CF4AFB8C512268AA0D824F351B903F7DBD04C99FEE20237FFABE6C3AA9623AF544E6AC4BC3B6742C0B6657D40D805579D9C4000BAA1FA5002BB2070036C66E63B08E2C8C60B5FB6E9C446F5FB15B31D82618040FEC541F2C0FBFFC51EB96B070B91D551C0BEAFACE9BA623BDF55B97C685FB44807893A021282E0736C85086338308310F80E636C72BEBCDAE646AB86434AF827350A44A351E398A685D0031A6B1BD1E9122B917542074CABE3D3A4457110536347E25E076C07DCC0811CA584BF591D1F8F279A40DD9ADBC82A1BD69E39162164A0860C72206D1DF462508176C291CE6E5C6113CE224D17100A64CE2E8980D49AED7D56684360562A477E60A12D0EA1D29ED25578F1428366C70A430977901294DAB77A4340AAD1E2740B8D3E028A19575012375A37784F4913906489184F10C82189464B315CD74E4894C224BA35386DB36A00BAB6C3A769BE7032B24FB57C65F0C8602D124D561416168723F0E46CA0932DA6010248DADAB287DE49597409466627F0D9C420F913A822B108250EE6D9131B66A0FB938EB3000FB42FBDBFEE02C54EBC82BC780A1F3AFABA147369EB7B9F370A446746F4862BC54CDF124776E1D0A55C0DB000F4A65E9C7234C74C19459510D0D8B782BEA382645DEDB10D8A6D221915D32F78252AFB7FB51B159F65DA41AD7FEE445C902FDE9249A96DEF74E6073F42B42C7E3EA1F350C527CF460C6B74DCE48005CC680EDEB1CCBC02B93F4BA045D9546B974B569943B2FA8AEED74553FB2343F147FE6491ADC91AB6443A2ACFAF574F57E1F9729ADEBBF2E4916DEF5204E0B983159335E575D9F37F16DD27A81712B6ABB7079A2AF8AEFBB09F2E03CCDC3DB827B15CD6B5228D5F1DD72F17B10ED8B2EAFB69FC8E64DFCEB3EDFEDF362CB64FB29620E53E944A69AFF7425ACF9F4D72A057BE6630BC532C3320BF8AFF1CB7D186DBA75BF06B2804B4094EF904DBAFAF25BE665DAFABB870ED2BB243604D4A0AF73AAFB40B6BBA80096FD1ADF04650D68FCDA0AF27B4BEE82F543F1FB97B0AA152403A2FF102CDA4F2FC3E02E0DB65903A31F5FFC59D0F066FBF56FFF0F9FF9949697230200 , N'6.1.3-40302')
