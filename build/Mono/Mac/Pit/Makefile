# Makefile for compiling and installing Pit MonoDevelop plugin on Mono
#   run 'make' to compile the plugin (dll + debug info)
#   run 'make install-addin' to copy the compiled plugin to MonoDevelop folders
#   run 'make install-fw' to copy the compiled plugin to MonoDevelop folders
#   run 'make package' to create a deployment binary package with addin (for repository)

# Here are a few paths that need to be configured first:
MDROOT  = /Applications/MonoDevelop.app/Contents/MacOS/lib/monodevelop
MAROOT  = /Library/Frameworks/Mono.framework/Versions/Current/lib/mono/mono-addins
MONOBIN = /Library/Frameworks/Mono.framework/Versions/Current/lib/mono/4.0
FSBIN = /Library/Frameworks/Mono.framework/Versions/Current/lib/mono/4.0
ATKDIR = /Library/Frameworks/Mono.framework/Versions/Current/lib/mono/gtk-sharp-2.0
PANGODIR = /Library/Frameworks/Mono.framework/Versions/Current/lib/mono/gtk-sharp-2.0
GTKDIR = /Library/Frameworks/Mono.framework/Versions/Current/lib/mono/gtk-sharp-2.0
GDKDIR = /Library/Frameworks/Mono.framework/Versions/Current/lib/mono/gtk-sharp-2.0
GLIBDIR = /Library/Frameworks/Mono.framework/Versions/Current/lib/mono/gtk-sharp-2.0
MONO = mono
MDRUN = $(MONO) $(MDROOT)/bin/mdtool.exe

FSC = fsharpc
CSC = gmcs
MDBIN = $(MDROOT)/bin

# Resources and files to be compiled/included as part of the project
RESOURCES = \
	--resource:MonoDevelop.Pit.addin.xml \
	--resource:MonoDevelop.Pit.PitLibraryProject.xpt.xml \

FILES = \
	PitProject.fs \
	PitProjectBinding.fs \
	

REFERENCES = \
	-r:$(MONOBIN)/mscorlib.dll \
	-r:$(MONOBIN)/Mono.Posix.dll \
	-r:$(MONOBIN)/System.Core.dll \
	-r:System.dll -r:System.Xml.dll \
	-r:$(MAROOT)/Mono.Addins.dll -r:Mono.Cairo.dll /codepage:65001 \
	-r:$(MDBIN)/MonoDevelop.Core.dll \
	-r:$(MDBIN)/MonoDevelop.Ide.dll \
	-r:$(MDBIN)/Mono.TextEditor.dll \
	-r:$(FSBIN)/FSharp.Core.dll \
	-r:$(FSBIN)/FSharp.Compiler.dll \
	-r:$(FSBIN)/FSharp.Compiler.Interactive.Settings.dll \
	-r:$(FSBIN)/FSharp.Compiler.Server.Shared.dll \
	-r:$(ATKDIR)/atk-sharp.dll \
	-r:$(PANGODIR)/pango-sharp.dll \
	-r:$(GTKDIR)/gtk-sharp.dll \
	-r:$(GDKDIR)/gdk-sharp.dll \
	-r:$(GLIBDIR)/glib-sharp.dll

OPTIONS = \
	--noframework --debug --optimize- --target:library --out:bin/MonoDevelop.Pit.dll

all:
	mkdir -p bin
	$(FSC) $(OPTIONS) $(REFERENCES) $(RESOURCES) $(FILES) 

install-fw:
	mkdir -p /Library/Frameworks/Pit/bin
	cp PitFw/*.* /Library/Frameworks/Pit/bin 

uninstall-fw:	
	rm /Library/Frameworks/Pit/bin/*.*

install-addin:
	cp bin/MonoDevelop.Pit.* $(MDROOT)/AddIns/BackendBindings/

uninstall-addin:
	rm $(MDROOT)/AddIns/BackendBindings/MonoDevelop.Pit.*

package:
	mkdir -p bin/repository
	mkdir -p repository
	cp bin/MonoDevelop.Pit.* bin/repository
	cp MonoDevelop.Pit.addin.xml bin/repository
	$(MDRUN) setup pack bin/repository/MonoDevelop.Pit.addin.xml -d:repository

