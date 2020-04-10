//export class gCategoryToIdeas {
//    CategoryId = 0;
//    IdeaId = 0;

//    constructor(categoryToIdeas = {
//        categoryId : 0,
//        ideaId : 0,
//    }) {
//        this.CategoryId = categoryToIdeas.categoryId;
//        this.IdeaId = categoryToIdeas.ideaId;
//    }
//}

export class gCategoryTag {
    Id = 0;
    Name = "";

    constructor(categoryTag = {
        id: 0,
        name: ""
    }) {
        this.Id = categoryTag.id;
        this.Name = categoryTag.name;
    }
}

export class gClosureDates {
    Year = 0;
    FirstClosure = 0;
    FinalClosure = 0;
    TimeStampLastModified = new Date();

    constructor(closureDates = {
        year : 0,
        firstClosure : 0,
        finalClosure : 0,
        timeStampLastModified : new Date(),
    }) {
        this.Year = closureDates.year;
        this.FirstClosure = closureDates.firstClosure;
        this.Finallosure = closureDates.finalClosure;
        this.TimeStampLastModified = closureDates.timeStampLastModified;
    }
}

export class gComment {
    Id = 0;
    Description = "";
    IsAnonymous = false;
    IdeaId = 0;
    User = new gUser();
    SubmissionDate = new Date();

    constructor(comment = {
        id : 0,
        description: "",
        isAnonymous: false,
        ideaId: 0,
        user: new gUser(),
        submissionDate: new Date(),
    }) {
        try {
            this.Id = comment.id;
            this.Description = comment.description;
            this.IsAnonymous = comment.isAnonymous;
            this.IdeaId = comment.ideaId;
            this.User = new gUser(comment.user);
            this.SubmissionDate = comment.submissionDate;
        } catch (e) {

        }
    }
}

export class gDepartment {
    Id = 0;
    Name = "";
    TotalNumberOfIdeas = 0;
    TotalPercentageOfIdeas = 0;
    TotalNumberOfContributors = 0;

    constructor(department = {
        id: 0,
        name: "",
        totalNumberOfIdeas: 0,
        totalPercentageOfIdeas: 0,
        totalNumberOfContributors: 0,
    }) {
        this.Id = department.id;
        this.Name = department.name;
        this.TotalNumberOfIdeas = department.totalNumberOfIdeas;
        this.TotalPercentageOfIdeas = department.totalPercentageOfIdeas;
        this.TotalNumberOfContributors = department.totalNumberOfContributors;
    }
}

export class gDocument {
    Id= 0;
    Name= "";
    IdeaId = 0;
    BlobStringBase64 = "";

    constructor(document = {
        id: 0,
        name: "",
        ideaId: 0,
        blobStringBase64: "",
    }) {
        this.Id = document.id;
        this.Name = document.name;
        this.Idea = document.ideaId;
        this.BlobStringBase64 = document.blobStringBase64;
    }
}

export class gFlaggedIdea {
    Id = 0;
    Type = "";
    Description = "";
    IdeaId = 0;
    UserId = 0;

    constructor(flaggedIdea = {
        id : 0,
        type : "",
        description: "",
        ideaId : 0,
        userId : 0,
    }) {
        this.Id = flaggedIdea.id;
        this.Type = flaggedIdea.type;
        this.Description = flaggedIdea.description;
        this.IdeaId = flaggedIdea.ideaId;
        this.UserId = flaggedIdea.userId;
    }
}

export class gIdea {
    Id = 0;
    Status = "";
    Title = "";
    ShortDescription = "";
    CreatedDate = new Date();
    FirstClosureDate = new Date();
    ClosureDate = new Date();
    IsAnonymous = false;
    ViewCount = 0;
    Author = "";
    FileBlobStringBase64 = "";
    Comments = [];
    FlaggedIdeas = [];
    Votes = [];
    CategoryTags = [];
    TotalThumbUps = 0;
    TotalThumbDowns = 0;

    constructor(idea = {
        id : 0,
        status : "",
        title : "",
        shortDescription : "",
        createdDate : new Date(),
        firstClosureDate : new Date(),
        closureDate : new Date(),
        isAnonymous : false,
        viewCount : 0,
        autor : "",
        fileBlobStringBase64 :"",
        comments :[],
        flaggedIdeas : [],
        votes :[],
        categoryTags :[],
        totalThumbUps : 0,
        totalThumbDowns : 0,
    }) {
        this.Id = idea.id;
        this.Status = idea.status;
        this.Title = idea.title;
        this.ShortDescription = idea.shortDescription;
        this.CreatedDate = idea.createdDate;
        this.FirstClosureDate = idea.firstClosureDate;
        this.ClosureDate = idea.closureDate;
        this.IsAnonymous = idea.isAnonymous;
        this.ViewCount = idea.viewCount;
        this.Author = new gUser(idea.author);
        this.FileBlobStringBase64 = idea.fileBlobStringBase64;
        this.Comments = idea.comments;
        this.FlaggedIdeas = idea.flaggedIdeas;
        this.Votes = idea.votes;
        this.CategoryTags = idea.categoryTags;
        this.TotalThumbUps = idea.totalThumbUps;
        this.TotalThumbDowns = idea.totalThumbDowns;
    }
}

export class gLoginRecord {
    Id = 0;
    TimeStamp = new Date();
    BrowserName = "";
    UserId = 0;

    constructor(loginRecord = {
        id: 0,
        timeStamp: new Date(),
        browserName: "",
        userId: new gUser(),
    }) {
        this.Id = loginRecord.id;
        this.TimeStamp = loginRecord.timeStamp;
        this.BrowserName = loginRecord.browserName;
        this.UserId = loginRecord.userId;
    }
}

export class gBrowser {
    Name = "";
    TotalHits = 0

    constructor(browser = {
        name: "",
        totalHits: 0
    }) {
        this.Name = browser.name;
        this.TotalHits = browser.totalHits;
    }
}

export class gPageView {
    Id = 0;
    PageName = "";
    PageCount = 0;

    constructor(pageView = {
        id: 0,
        pageName: "",
        pageCount: 0,
    }) {
        this.Id = pageView.id;
        this.PageName = pageView.pageName;
        this.PageCount = pageView.pageCount;
    }
}

export class gRole {
    Id = 0;
    Name = "";
    AccessClaim = "";

    constructor(role = {
        id: 0,
        name: "",
        accessClaim: "",
    }) {
        this.Id = role.id;
        this.Name = role.name;
        this.AccessClaim = role.accessClaim;
    }
}

export class gUser {
    Id = 0;
    FirstName = "";
    Surname = "";
    Email = "";
    Department = new gDepartment();
    Role = new gRole();
    IsBlocked= false;
    Ideas = [];
    Comments = [];
    FlaggedIdeas = [];
    LoginRecords = new Date();
    LastLoginDate = Date.UTC;
    NewPassword = "";
    TotalNumberOfIdeas = 0;
    TotalNumberOfComments = 0;
    constructor(user = {
        id : 0,
        firstName : "",
        surname : "",
        email : "",

        department : new gDepartment(),
        role : new gRole(),
        isBlocked : false,
        ideas :[],
        comments :[],
        flaggedIdeas :[],
        loginRecords :[],

        lastLoginDate : new Date(),
        newPassword : "",
        totalNumberOfIdeas : 0,
        totalNumberOfComments : 0,
    }) {
        try {
            this.Id = user.id;
            this.FirstName = user.firstName;
            this.Surname = user.surname;
            this.Email = user.email;
            this.Department = new gDepartment(user.department);
            this.Role = new gRole(user.role);
            this.IsBlocked = user.isBlocked;
            this.Ideas = user.ideas;
            this.Comments = user.comments;
            this.FlaggedIdeas = user.flaggedIdeas;
            this.LoginRecords = user.loginRecords;
            this.LastLoginDate = user.lastLoginDate;
            this.NewPassword = user.newPassword;
            this.TotalNumberOfIdeas = user.totalNumberOfIdeas;
            this.TotalNumberOfComments = user.totalNumberOfComments;
        } catch (e) {

        }
    }
}

export class gError {
    Key = "";
    Value = "";

    constructor(error = {
        key: "",
        value: ""
    }) {
        this.Key = error.key;
        this.Value = error.value;
    }
}