namespace Shared

open System

type LectureMetaData =
    { Date : string
      Title : string
      Tags : string list
      Menu : string list
      Path : string
    }
type Dir = string
type Path = string
type LectureContent = string
type Lecture = LectureMetaData * LectureContent


module Route =
    let builder typeName methodName =
        sprintf "/api/%s/%s" typeName methodName

type IApi =
    { GetAllLectureMetaData : Dir -> Async<LectureMetaData list>
      GetLecture : Path -> Async<LectureMetaData * LectureContent>
    }
