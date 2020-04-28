# Code for isosurface extraction
# NOTE: This code is heavily based on the code from the following site:
# https://www.evl.uic.edu/aspale/cs526/final/3-5-2-0.htm 

import vtk
import sys

def main(argv):
    
    fileName = argv[1]
    outputName = argv[2]
    isoValueLo = int(argv[3])
    isoValueHi = int(argv[4])

    # Prepare to read the file
    readerVolume = vtk.vtkTIFFReader()
    readerVolume.SetFileName( fileName )
    readerVolume.Update()

    # Generate an isosurface
    contour = vtk.vtkMarchingCubes()
    contour.SetInputConnection( readerVolume.GetOutputPort() )
    contour.ComputeNormalsOn()
    contour.SetValue( isoValueLo, isoValueHi )  # Isovalue
    contour.Update()

    # Take the isosurface data and create geometry
    geoMapper = vtk.vtkPolyDataMapper()
    geoMapper.SetInputConnection( contour.GetOutputPort() )
    geoMapper.ScalarVisibilityOff()

    # Take the isosurface data and create geometry
    actor = vtk.vtkLODActor()
    #actor.SetNumberOfCloudPoints( 1000000 )
    actor.SetMapper( geoMapper )
    actor.GetProperty().SetColor( 1, 1, 1 )

    # Create renderer
    ren = vtk.vtkRenderer()
    ren.SetBackground( 0.329412, 0.34902, 0.427451 ) #Paraview blue
    ren.AddActor(actor)

    # Create a window for the renderer of size 250x250
    renWin = vtk.vtkRenderWindow()
    renWin.AddRenderer(ren)
    renWin.SetSize(500, 500)

    # Set an user interface interactor for the render window
    iren = vtk.vtkRenderWindowInteractor()
    iren.SetRenderWindow(renWin)
    
    # Start the initialization and rendering
    iren.Initialize()
    renWin.Render()

    # Write isoextracted surface to output file
    writer = vtk.vtkOBJExporter()
    writer.SetInput(renWin)
    writer.SetFilePrefix(outputName.split('.tif')[0])
    writer.Write()

    iren.Start()



if __name__ == '__main__':
    if len(sys.argv) != 5:
        print("ARGS: isoextract.py [INPUT_NAME] [OUTPUT_NAME] [ISOVALUE_LOW] [ISOVALUE_HIGH]")
    else:
        main(sys.argv)
