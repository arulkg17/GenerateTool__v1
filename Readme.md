// List of ignored files
git status --ignored

//List only ignored files
git ls-files --others --ignored --exclude-standard

//Show all tracked files
git ls-files

If you added .vs, bin, or obj before adding them to .gitignore, Git will continue tracking them. Remove them from the Git index (cache) like this:

git rm -r --cached .vs
git rm -r --cached bin
git rm -r --cached obj


//Then commit the change:

git commit -m "Stop tracking ignored files"
git push

//This is useful after updating your .gitignore.

git rm -r --cached .
git add .
git commit -m "Refresh Git index"
