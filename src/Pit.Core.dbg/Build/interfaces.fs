namespace Pit
// type alias for major interfaces from the .NET library
// easy to get all this under the "Pit" namespace itself
type IDisposable     = System.IDisposable
type IObservable<'T> = System.IObservable<'T>
type IObserver<'T>   = System.IObserver<'T>
type IEnumerable<'T> = System.Collections.Generic.IEnumerable<'T>
type IEnumerable     = System.Collections.IEnumerable
type IEnumerator<'T> = System.Collections.Generic.IEnumerator<'T>
type IEnumerator     = System.Collections.IEnumerator