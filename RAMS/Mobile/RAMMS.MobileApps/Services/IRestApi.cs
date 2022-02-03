using RAMMS.Domain;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.MobileApps.Interface;
using RAMMS.MobileApps.Model;
using RAMMS.MobileApps.Model.Adapter;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RAMMS.MobileApps
{
    [Headers("Accept: */* , Content-Type: application/json")]
    public interface IRestApi
    {

        [Post("/api/signin")]
        Task<ResponseBaseObject<string>> SignIn([Body] RmUserCredential formObject);

        [Post("/api/forgetPassword")]
        Task<ResponseBaseObject<int>> GetPasswordReset(string path , string email);

        [Post("/api/login")]
        Task<ResponseBaseObject<RmUsers>> UserLoginAccount([Body] RmUserCredential formObject);

        [Get("/api/ddMonthlist")]
        Task<ResponseBaseListObject<DDListItems>> GetDDMonthList();

        [Get("/api/ddWeeklist")]
        Task<ResponseBaseListObject<DDListItems>> GetDDWeekList();

        [Post("/api/ddlist")]
        Task<ResponseBaseListObject<DDListItems>> GetDDList([Body] DDLookUpDTO formObject);

        [Post("/api/ddldescvalue")]
        Task<ResponseBaseListObject<DDListItems>> GetDDLDescValue([Body] DDLookUpDTO ddListObj);

        [Post("/api/ddldescvalueconcat")]
        Task<ResponseBaseListObject<DDListItems>> GetDDLDescValueConcat([Body] DDLookUpDTO ddListObj);

        [Get("/api/crewSupervisor")]
        Task<ResponseBaseListObject<DDListItems>> GetSupervisor();

        //[Post("/api/getdtlform")]
        //Task<ResponseBaseListObject<RmFormADtl>> GetFormADetailGridFormList([Body] RmFormADtl formObject);

        //[Post("/api/saveImageform")]
        //Task<ResponseBaseListObject<RmFormaImageDtl>> GetFormAImageSaveItem([Body] List<RmFormaImageDtl> formObject);

        //[Post("/api/gethdrgrid")]
        //Task<ResponseBaseListObject<RmFormAHdr>> GetFormAHeaderGridListItem([Body] RmFormAHdr hdrformObject);

        //[Post("/api/savedtlform")]
        //Task<ResponseBaseObject<int>> GetFormASaveItem([Body] RmFormADtl dtlformObject);

        //[Post("/api/savehdrform")]
        //Task<ResponseBaseObject<int>> GetFormAhdrSaveItem([Body] RmFormAHdr dtlformObject);

        //[Post("/api/smartsearch")]
        //Task<ResponseBaseListObject<RmFormAHdr>> GetFormASmartSearchItem([Body] RmFormAHdr dtlformObject);

        //[Post("/api/detailsearch")]
        //Task<ResponseBaseListObject<RmFormAHdr>> GetFormADetailSearchItem([Body] RmFormAHdr dtlformObject);

        ////[Post("/api/gethdrview")]
        //Task<ResponseBaseObject<RmFormAHdr>> GetGridFormAHeaderViewGridview([Body] RmFormAHdr hdrformObject);

        //[Post("/api/getdtlview")]
        //Task<ResponseBaseObject<RmFormADtl>> GetGridFormADetailViewGridview([Body] RmFormADtl hdrformObject);





        //New API
        [Post("/api/formALandingGrid")]
        Task<ResponseBaseObject<PagingResult<FormAHeaderResponseDTO>>> GetGridFormALandingGridview([Body] FilteredPagingDefinition<FormASearchGridDTO> hdrformObject);

        [Post("/api/GetFormJLandingGrid")]
        Task<ResponseBaseObject<PagingResult<FormJSearchGridDTO>>> GetGridFormJLandingGridview([Body] FilteredPagingDefinition<FormJSearchGridDTO> formJSearchDTO );
        [Post("/api/getrmroadcode")]
        Task<ResponseBaseObject<RoadMasterResponseDTO>> GetRM_RoadCode_Service([Body] RoadMasterRequestDTO _Rmroad);

        [Post("/api/RefNo")]
        Task<ResponseBaseObject<string>> GetReferenceNOData(string roadCode, int month, string year, string assetGroup);

        [Post("/api/SaveFormADtl")]
        Task<ResponseBaseObject<int>> SaveFormADtl([Body] FormADetailsRequestDTO detailDto);

        [Post("/api/SaveFormAHdr")]
        Task<ResponseBaseObject<FormAHeaderResponseDTO>> SaveFormAHdr([Body] FormAHeaderRequestDTO detailDto);

        
        [Post("/api/AssetGroupByNameFormA")]
        Task<ResponseBaseObject<string>> GetAssetCodeByNameFormA(string assetGroup);



        [Post("/api/DetailSrNo")]
        Task<ResponseBaseObject<int>> DetailSrNo(int headerRef);


        [Post("/api/ddlcodeDesc")]
        Task<ResponseBaseListObject<DDListItems>> GetTypeCodeAndValue([Body] DDLookUpDTO formObject);

        [Post("/api/distressCode")]
        Task<ResponseBaseListObject<DDListItems>> GetDistressCode(string assetGroup);


        [Post("/api/getFormADetails")]
        Task<ResponseBaseObject<FormAHeaderRequestDTO>> GetFormAUpdateAndView(int SaveObj);

        // Get Header with Header ID
        [Post("/api/GetFormAHeaderById")]
        Task<ResponseBaseObject<FormAHeaderResponseDTO>> GetFormAHdrById(int headerId);


        //Get Details Based on Header id 
        [Post("/api/DetailGrid")]
        Task<ResponseBaseObject<PagingResult<FormADetailResponseDTO>>> GetDetails([Body] FilteredPagingDefinition<FormADetailsRequestDTO> GetFormhdrObj);


        // Get Single Detail For Edit and View
        [Post("/api/GetFormADetailById")]
        Task<ResponseBaseObject<FormADetailsRequestDTO>> GetFormADetail(int detailId);


        // Update Detail
        [Post("/api/updateFormA")]
        Task<ResponseBaseObject<int>> FormAUpdate([Body] FormADetailsRequestDTO SaveObj);

        //FormA Delete Image
        [Post("/api/deleteImage")]
        Task<ResponseBaseObject<int>> DeleteImage(int imageId);

        //Delete Header Details
        [Post("/api/deleteDetail")]
        Task<ResponseBaseObject<int>> DeleteDetail(int detailId);

        //Delete Detail
        [Post("/api/deletehdr")]
        Task<ResponseBaseObject<int>> DeleteHeader(int headerNo);

        //Delete Detail
        [Post("/api/updateSignature")]
        Task<ResponseBaseObject<int>> UpdateSignature([Body] FormAHeaderRequestDTO GetFormDtlObj);


        [Get("/api/GetInsUser")]
        Task<ResponseBaseListObject<DDListItems>> userList();


        //Delete Detail
        [Post("/api/user")]
        Task<ResponseBaseObject<UserResponseDTO>> UserDtl([Body] UserRequestDTO UserObj);


        [Post("/api/imageUploadFormA")]
        Task<ResponseBaseObject<string>> ImageUploaded([Body] List<ImageUploadFormATABDTO> FormFile, int AssetId, string PhotoType);


        [Post("/api/landingDropdown")]
        Task<ResponseBaseObject<AssetDDLResponseDTO>> GetLandingDropDown([Body] AssetDDLRequestDTO formA);

        [Post("/api/formF2Dropdown")]
        Task<ResponseBaseObject<AssetDDLResponseDTO>> GetF2LandingDropDown([Body] AssetDDLRequestDTO request);

        [Post("/api/formB1B2Dropdown")]
        Task<ResponseBaseObject<AssetDDLResponseDTO>> GetB1B2LandingDropDown([Body] AssetDDLRequestDTO request);


        [Get("/api/ddlRoadCodeConcat")]
        Task<ResponseBaseListObject<DDListItems>> GetConcatCodeValue();

        //FORM D
        [Post("/api/getformDgridlist")]
        Task<ResponseBaseObject<PagingResult<FormDHeaderResponseDTO>>> GetFormDGridList([Body] FilteredPagingDefinition<FormDSearchGridDTO> hdrformObject);

        [Post("/api/SaveFormDHdr")]
        Task<ResponseBaseObject<int>> SaveFormDHdr([Body] FormDHeaderRequestDTO SaveObj);

        [Post("/api/formDFindDetails")]
        Task<ResponseBaseObject<FormDHeaderRequestDTO>> FindAndSaveFormDHdr([Body] FormDHeaderRequestDTO SaveObj);


        [Post("/api/sectionByRmuMaster")]
        Task<ResponseBaseListObject<DDListItems>> GetSectionByRmu(string rmu);


        // Update Detail
        [Post("/api/UpdateFormDHdr")]
        Task<ResponseBaseObject<int>> UpdateFormDHdr([Body] FormDHeaderRequestDTO SaveObj);

        //Delete Header Details
        [Post("/api/DeleteFormDHdr")]
        Task<ResponseBaseObject<int>> DeleteFormDHdr(int formDNo);

        //ErtActivityCode
        [Get("/api/ErtActivityCode")]
        Task<ResponseBaseListObject<DDListItems>> GetErtActivityCode();



        [Post("/api/RodeCodeByRmu")]
        Task<ResponseBaseListObject<DDListItems>> RodeCodeByRmu(string rmu);

        [Post("/api/roadcodebySection")]
        Task<ResponseBaseListObject<DDListItems>> RodeCodeBySection(string seccode);



        // Get Header with Header ID
        [Post("/api/GetFormAHeaderById")]
        Task<ResponseBaseObject<bool>> CheckFormDHdrRefid(string formdId);



        [Get("/api/GetUserDetails")]
        Task<UserDetails> GetUserDetails();


        [Post("/api/FormDRefNo")]
        Task<ResponseBaseObject<string>> GetFormDRefNo(int? weekNo, int? monthNo, int? year, string crewUnit,string day,string rmu,string secCode);


        [Post("/api/getformDLabourgridlist")]
        Task<ResponseBaseObject<PagingResult<FormDLabourDetailsResponseDTO>>> GetFormDLabourGridList([Body] FilteredPagingDefinition<FormDSearchGridDTO> hdrformObject, string HdrId);

        [Post("/api/SaveFormDLabourHdr")]
        Task<ResponseBaseObject<int>> SaveFormDLabourHdr([Body] FormDLabourDetailsRequestDTO SaveObj);

        [Post("/api/formDRefNoGeneration")]
        Task<ResponseBaseObject<string>> GenerateFormDRefNo([Body] FormDHeaderRequestDTO requestDTO);


        // Update UpdateFormDLabourHdr
        [Post("/api/UpdateFormDLabourHdr")]
        Task<ResponseBaseObject<int>> UpdateFormDLabourHdr([Body] FormDLabourDetailsRequestDTO SaveObj);

        //Delete FormD Labour Header Details
        [Post("/api/DeleteFormDLabourHdr")]
        Task<ResponseBaseObject<int>> DeleteFormDLabourHdr(int formDNo);

        [Post("/api/EditViewFormDHdr")]
        Task<ResponseBaseObject<FormDHeaderRequestDTO>> FormDGetById(int formdId);





        //Edit FormD LabourHdr Details
        [Post("/api/EditViewFormDLabourHdr")]
        Task<ResponseBaseObject<FormDLabourDetailsRequestDTO>> FormDLabourGetById(int formdId);


        //ErtActivityCode
        [Get("/api/LabourCode")]
        Task<ResponseBaseListObject<DDListItems>> GetFormDLabourCode();

        [Post("/api/LabourSrNo")]
        Task<ResponseBaseObject<int>> FormDLabourSrNo(int HdrId);



        //Equement details
        [Post("/api/getformDEqpgridlist")]
        Task<ResponseBaseObject<PagingResult<FormDEquipDetailsResponseDTO>>> GetFormDEqpGridList([Body] FilteredPagingDefinition<FormDSearchGridDTO> hdrformObject, string HdrId);

        [Post("/api/SaveFormDEqpHdr")]
        Task<ResponseBaseObject<int>> SaveFormDEqpHdr([Body] FormDEquipRequestDTO SaveObj);


        // Update UpdateFormDLabourHdr
        [Post("/api/UpdateFormDEqpHdr")]
        Task<ResponseBaseObject<int>> UpdateFormDEqpHdr([Body] FormDEquipRequestDTO SaveObj);

        //Delete FormD Labour Header Details
        [Post("/api/DeleteFormDEqpHdr")]
        Task<ResponseBaseObject<int>> DeleteFormDEqpHdr(int formDNo);

        //Edit FormD LabourHdr Details
        [Post("/api/EditViewFormDEqpHdr")]
        Task<ResponseBaseObject<FormDEquipRequestDTO>> FormDEqpGetById(int formdId);


        //ErtActivityCode
        [Get("/api/EqpCode")]
        Task<ResponseBaseListObject<DDListItems>> GetFormDEqpCode();

        [Post("/api/EqpSrNo")]
        Task<ResponseBaseObject<int>> FormDEqpSrNo(int HdrId);




        //Meterials
        [Post("/api/getformDMaterialgridlist")]
        Task<ResponseBaseObject<PagingResult<FormDMaterialDetailsResponseDTO>>> GetFormDMaterialGridList([Body] FilteredPagingDefinition<FormDSearchGridDTO> hdrformObject, string HdrId);

        [Post("/api/SaveFormDMaterialHdr")]
        Task<ResponseBaseObject<int>> SaveFormDMaterialHdr([Body] FormDMaterialDetailsRequestDTO SaveObj);


        // Update UpdateFormDLabourHdr
        [Post("/api/UpdateFormDMaterialHdr")]
        Task<ResponseBaseObject<int>> UpdateFormDMaterialHdr([Body] FormDMaterialDetailsRequestDTO SaveObj);

        //Delete FormD Labour Header Details
        [Post("/api/DeleteFormDMaterialHdr")]
        Task<ResponseBaseObject<int>> DeleteFormDmaterialHdr(int formDNo);

        //Edit FormD LabourHdr Details
        [Post("/api/EditViewFormDMaterialHdr")]
        Task<ResponseBaseObject<FormDMaterialDetailsRequestDTO>> FormDMaterialGetById(int formdId);


        //ErtActivityCode
        [Get("/api/MaterialCode")]
        Task<ResponseBaseListObject<DDListItems>> GetFormDMaterialCode();

        [Post("/api/MaterialSrNo")]
        Task<ResponseBaseObject<int>> FormDMaterialSrNo(int HdrId);





        [Post("/api/getformDDtlgridlist")]
        Task<ResponseBaseObject<PagingResult<FormDDetailsResponseDTO>>> GetFormDDtlGridList([Body] FilteredPagingDefinition<FormDSearchGridDTO> hdrformObject, string HdrId);

        [Post("/api/SaveFormDDtl")]
        Task<ResponseBaseObject<int>> SaveFormDDtl([Body] FormDDetailsRequestDTO SaveObj);


        // Update UpdateFormDLabourHdr
        [Post("/api/UpdateFormDDtl")]
        Task<ResponseBaseObject<int>> UpdateFormDDtl([Body] FormDDetailsRequestDTO SaveObj);

        //Delete FormD Labour Header Details
        [Post("/api/DeleteFormDDtl")]
        Task<ResponseBaseObject<int>> DeleteFormDDtl(int formDNo);

        //Edit FormD LabourHdr Details
        [Post("/api/EditViewFormDDtl")]
        Task<ResponseBaseObject<FormDDetailsRequestDTO>> FormDDtlGetById(int formdId);


        [Post("/api/DtlFromDSrNo")]
        Task<ResponseBaseObject<int>> FormDDtlSrNo(int HdrId);



        [Post("/api/FormDupdateSignature")]
        Task<ResponseBaseObject<int>> UpdateSignatureFormD([Body] FormDHeaderRequestDTO GetFormDtlObj);

        [Post("/api/FormXRefForFormD")]
        Task<ResponseBaseListObject<DDListItems>> GetFormXRefForFormD(string rodeCode);

        [Post("/api/FormDGetImage")]
        Task<ResponseBaseListObject<WarImageDtlResponseDTO>> GetImagesFormD(string AssetId);

        [Post("/api/FormDGetAccUcc")]
        Task<ResponseBaseListObject<AccUccImageDtlResponseDTO>> GetAccUccFormD(string AssetId);

        [Post("/api/deleteImageFormD")]
        Task<ResponseBaseObject<int>> DeleteImageFormD(int imageId);


        [Post("/api/deleteACCUCCFormD")]
        Task<ResponseBaseObject<int>> DeleteAccUccFormD(int imageId);



        [Post("/api/FormJGetImage")]
        Task<ResponseBaseListObject<FormJImageListResponseDTO>> GetImagesFormJ(string AssetId);

        //FormJ Delete Image
        [Post("/api/deleteImageFormJ")]
        Task<ResponseBaseObject<int>> DeleteImageFormJ(int imageId);

        //FormJ Get Detail Serial No
        [Post("/api/GetFormJDtlSrno")]
        Task<ResponseBaseObject<int?>> GetDetailSerialNoFormJ(int headerId);

        [Post("/api/GetAssetCodeByNameJ")]
        Task<ResponseBaseObject<string>> GetAssetCodeByNameFormJ(string name);


        //Form B1/B2 end points

        //B1B2 GetLandingGrid
        [Post("/api/getB1B2GridData")]
        Task<ResponseBaseObject<PagingResult<FormB1B2HeaderRequestDTO>>> GetFormB1B2LandingGridData([Body] FilteredPagingDefinition<FormB1B2SearchGridDTO> landingGrid);

        [Post("/api/getB1B2BridgeData")]
        Task<ResponseBaseListObject<DDListItems>> GetDDBridgeList([Body] AssetDDLRequestDTO request);

        [Post("/api/saveB1B2Header")]
        Task<ResponseBaseObject<FormB1B2HeaderRequestDTO>> SaveB1B2Hdr([Body] FormB1B2HeaderRequestDTO data);

        [Post("/api/updateB1B2")]
        Task<ResponseBaseObject<int>> UpdateB1B2([Body] FormB1B2HeaderRequestDTO data);

        [Post("/api/getB1B2ById")]
        Task<ResponseBaseObject<FormB1B2HeaderRequestDTO>> GetB1B2ById(int BridgeId);

        [Post("/api/getB1B2Images")]
        Task<ResponseBaseListObject<FormB1B2ImgRequestDTO>> GetB1B2Images(int assetId);

        [Post("/api/deactivateb1b2Image")]
        Task<ResponseBaseObject<int>> DeleteB1B2Image(int imageId);

        [Post("/api/deleteHdrB1B2")]
        Task<ResponseBaseObject<bool>> DeleteB1B2Hdr(int id);


        //Form F2 end points

        //F2 GetLandingGrid
        [Post("/api/getF2GridData")]
        Task<ResponseBaseObject<PagingResult<FormF2HeaderRequestDTO>>> GetFormF2LandingGridData([Body] FilteredPagingDefinition<FormF2SearchGridDTO> landingGrid);

        [Post("/api/roadLength")]
        Task<ResponseBaseObject<decimal?>> GetRoadLength(string roadcode);

        [Post("/api/formF2FindDetails")]
        Task<ResponseBaseObject<FormF2HeaderRequestDTO>> SaveF2Hdr([Body] FormF2HeaderRequestDTO model);

        [Post("/api/GetF2DetailList")]
        Task<ResponseBaseObject<PagingResult<FormF2DetailRequestDTO>>> GetF2DetailList([Body] FilteredPagingDefinition<FormF2DetailRequestDTO> searchData);
        
        [Post("/api/GetF2HeaderById")]
        Task<ResponseBaseObject<FormF2HeaderRequestDTO>> GetF2HeaderById(int id);

        [Post("/api/updateF2Header")]
        Task<ResponseBaseObject<int>> SaveF2Signature([Body] FormF2HeaderRequestDTO model);

        [Post("/api/deleteHeader")]
        Task<ResponseBaseObject<bool>> DeleteF2Header(int id);

        [Post("/api/deleteF2Dtl")]
        Task<ResponseBaseObject<bool>> DeleteF2Detail(int id);

        [Post("/api/SaveF2Dtl")]
        Task<ResponseBaseObject<int>> SaveF2Detail([Body] FormF2DetailRequestDTO model);

        //C1C2 End Points

        [Post("/api/getC1C2GridData")]
        Task<ResponseBaseObject<GridWrapper<object>>> GetFormC1C2LandingGridData([Body] SearchRequestDTO landingGrid);

        [Post("/api/deleteC1C2")]
        Task<ResponseBaseObject<int>> DeleteC1C2Header(int id);
        
        [Post("/api/findDetailsC1C2")]
        Task<ResponseBaseObject<FormC1C2HeaderRequestDTO>> SaveC1C2Hdr([Body] FormC1C2HeaderRequestDTO frmC1C2);

        [Post("/api/updateC1C2")]
        Task<ResponseBaseObject<int>> UpdateC1C2([Body] FormC1C2HeaderRequestDTO data);

        [Post("/api/getC1C2ById")]
        Task<ResponseBaseObject<FormC1C2HeaderRequestDTO>> GetC1C2ById(int id);

        [Post("/api/getC1C2List")]
        Task<ResponseBaseListObject<DDListItems>> GetDDAssetList([Body] AssetDDLRequestDTO request);

        [Post("/api/getC1C2ImageList")]
        Task<ResponseBaseListObject<FormC1C2ImgRequestDTO>> GetC1C2Images(int id);

        [Post("/api/deleteC1C2Image")]
        Task<ResponseBaseObject<int>> DeleteC1C2Image(int headerid, int imgId);

        //FC End Points

        [Post("/api/getFCGridData")]
        Task<ResponseBaseObject<GridWrapper<object>>> GetFormFCLandingGridData([Body] SearchRequestDTO landingGrid);

        [Post("/api/deleteFC")]
        Task<ResponseBaseObject<int>> DeleteFCHeader(int id);

        [Post("/api/findDetailsFC")]
        Task<ResponseBaseObject<FormFCHeaderRequestDTO>> SaveFCHdr([Body] FormFCHeaderRequestDTO frmFC);

        [Post("/api/AssetCheckFC")]
        Task<ResponseBaseObject<bool>> AssetCheck(string roadCode);



        [Post("/api/updateFC")]
        Task<ResponseBaseObject<int>> UpdateFC([Body] FormFCHeaderRequestDTO frmFC);

        [Post("/api/getFCById")]
        Task<ResponseBaseObject<FormFCHeaderRequestDTO>> GetFCById(int id);

        //FD End Points

        [Post("/api/getFDGridData")]
        Task<ResponseBaseObject<GridWrapper<object>>> GetFormFDLandingGridData([Body] SearchRequestDTO landingGrid);

        [Post("/api/deleteFD")]
        Task<ResponseBaseObject<int>> DeleteFDHeader(int id);

        [Post("/api/findDetailsFD")]
        Task<ResponseBaseObject<FormFDHeaderRequestDTO>> SaveFDHdr([Body] FormFDHeaderRequestDTO frmFD);

        [Post("/api/AssetCheckFD")]
        Task<ResponseBaseObject<bool>> AssetCheckFd(string roadCode);


        [Post("/api/updateFD")]
        Task<ResponseBaseObject<int>> UpdateFD([Body] FormFDHeaderRequestDTO frmFD);

        [Post("/api/getFDById")]
        Task<ResponseBaseObject<FormFDHeaderRequestDTO>> GetFDById(int id);



        //Form X
        //*******
        //Landing Grid
        [Post("/api/getformxgridlist")]
        Task<ResponseBaseObject<PagingResult<FormXHeaderResponseDTO>>> GetFormXGridList([Body] FilteredPagingDefinition<FormXSearchGridDTO> formxsearchgrid);

        //Gets the list of main task and sub task
        [Post("/api/ddlcodeValueConcat")]
        Task<ResponseBaseListObject<DDListItems>> GetTaskList([Body] DDLookUpDTO formObject);

        //Gets detailed records
        [Post("/api/getFormXById")]
        Task<ResponseBaseObject<FormXHeaderRequestDTO>> FormXDetailsById(int formxId);

        //Gets the list of war images
        [Post("/api/getFormXWarImageList")]
        Task<ResponseBaseListObject<WarImageDtlResponseDTO>> GetFormXWarImageList(int formXId);

        //Gets the list of U see U images
        [Post("/api/getFormXUSeeU")]
        Task<ResponseBaseListObject<AccUccImageDtlResponseDTO>> GetFormXUseeUImageList(int formXId);

        //Dashboard
        [Post("/api/homeSectionDrop")]
        Task<ResponseBaseListObject<DDListItems>> GetSectionbyRMU([Body] LandingHomeRequestDTO landingHomeRequestDTO);
            
        [Post("/api/homeNodCount")]
        Task<ResponseBaseObject<LandingHomeResponseDTO>> GetNodClosedResult([Body] LandingHomeRequestDTO landingHomeRequestDTO);

    }
}